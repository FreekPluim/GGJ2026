using SadUtils;
using System;
using System.Collections;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private enum FacingDirection
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    [SerializeField] KeybindsSO keybinds;
    public MaskHandler maskHandler;
    [SerializeField] Animator animator;

    [Space]
    [SerializeField] FacingDirection facingDirection = FacingDirection.Down;
    // Differ from the facingDir so that it updates on frame 1
    private FacingDirection lastFacingDirection = FacingDirection.Up;
    private int lastMaskID = -1;

    [Header("Settings")]
    [SerializeField] private Transform visuals;
    [SerializeField] private float bonkAnimHoldTime;
    [SerializeField] private float bonkAnimMoveSpeed;
    [SerializeField] private float bonkDistance;
    private Vector3 lastBonkAnimStartPos;
    private Coroutine bonkRoutine;

    Vector3Int currentTile;

    public static Action<Vector3Int, GameObject> OnPositionChanged;

    int facingDirAnimId;
    int maskIDAnimId;

    protected override void Awake()
    {
        // Cache Animator Ids
        facingDirAnimId = Animator.StringToHash("LookDirection");
        maskIDAnimId = Animator.StringToHash("MaskID");
    }

    private void Start()
    {
        currentTile = MapManager.Instance.GetGridPositionFromPosition(transform.position);
        transform.position = currentTile;
        maskHandler = GetComponent<MaskHandler>();
    }
    private void Update()
    {
        if (MapManager.Instance != null)
        {
            bool gotMoveInput = false;
            if (Input.GetKeyDown(keybinds.Right))
            {
                gotMoveInput = true;
                TryMove(Vector3Int.right);
                facingDirection = FacingDirection.Right;
            }
            if (Input.GetKeyDown(keybinds.Left))
            {
                gotMoveInput = true;
                TryMove(Vector3Int.left);
                facingDirection = FacingDirection.Left;
            }
            if (Input.GetKeyDown(keybinds.Up))
            {
                gotMoveInput = true;
                TryMove(Vector3Int.up);
                facingDirection = FacingDirection.Up;
            }
            if (Input.GetKeyDown(keybinds.Down))
            {
                gotMoveInput = true;
                TryMove(Vector3Int.down);
                facingDirection = FacingDirection.Down;
            }
            if (gotMoveInput) { HandleBonk(); }

            TryUseMaskAbility();
        }

        UpdateAnimationStates();
    }

    private void TryMove(Vector3Int direction)
    {
        if (!MapManager.Instance.CheckIsWalkable(currentTile + direction)) { return; }
        if (MapManager.Instance.GetOccupiedTile(currentTile + direction) != null)
        {
            if (MapManager.Instance.GetOccupiedTile(currentTile + direction).walkable)
            {
                currentTile += direction;
                transform.position = currentTile;
                OnPositionChanged?.Invoke(currentTile, gameObject);
                return;
            }
            if (maskHandler.GetActiveMask().Type != Mask.MaskType.Strength) return;
            if (MapManager.Instance.GetOccupiedTile(currentTile + direction).TryMove(direction))
            {
                //MoveSelf
                currentTile += direction;
                transform.position = currentTile;
                OnPositionChanged?.Invoke(currentTile, gameObject);
                return;
            }
        }
        else
        {
            currentTile += direction;
            transform.position = currentTile;
            OnPositionChanged?.Invoke(currentTile, gameObject);
            return;
        }
    }

    private void TryDash()
    {
        Vector3Int facingVector = FaceDirToVector(facingDirection);
        //Check if block between
        if (!MapManager.Instance.CheckIsWalkable(currentTile + facingVector) && !MapManager.Instance.GetTileDataFromCell(currentTile + facingVector).Skippable) return;

        //Check if block you try dash on is walkable from occupied space perspective
        if (MapManager.Instance.GetOccupiedTile(currentTile + (facingVector)) != null && !MapManager.Instance.GetOccupiedTile(currentTile + (facingVector)).walkable) return;
        if (MapManager.Instance.GetOccupiedTile(currentTile + (facingVector * 2)) != null && !MapManager.Instance.GetOccupiedTile(currentTile + (facingVector * 2)).walkable) return;

        //Check if block you try dash on is walkable
        if (MapManager.Instance.CheckIsWalkable(currentTile + (facingVector * 2)))
        {
            currentTile += (facingVector * 2);
            transform.position = currentTile;
            OnPositionChanged?.Invoke(currentTile);
            return;
        }
    }
    void TryUseMaskAbility()
    {
        if (Input.GetKeyDown(keybinds.useMask))
        {
            switch (maskHandler.GetActiveMask().Type)
            {
                case Mask.MaskType.Dash:

                    if (maskHandler.GetActiveMask().CanUse)
                    {
                        TryDash();
                        HandleBonk();
                    }

                    break;
                default:
                    break;
            }
        }
    }

    private void UpdateAnimationStates()
    {
        UpdateFacingDirAnimParam();
        UpdateMaskIDAnimParam();
    }

    private void UpdateFacingDirAnimParam()
    {
        if (facingDirection == lastFacingDirection)
        {
            return;
        }

        animator.SetInteger(facingDirAnimId, (int)facingDirection);
        lastFacingDirection = facingDirection;
    }

    private void UpdateMaskIDAnimParam()
    {
        int maskID = (int)maskHandler.GetActiveMask().Type;
        if (maskID == lastMaskID)
        {
            return;
        }

        animator.SetInteger(maskIDAnimId, maskID);
        lastMaskID = maskID;
    }

    private Vector3Int FaceDirToVector(FacingDirection facingDirection)
    {
        return facingDirection switch
        {
            FacingDirection.Up => Vector3Int.up,
            FacingDirection.Right => Vector3Int.right,
            FacingDirection.Down => Vector3Int.down,
            FacingDirection.Left => Vector3Int.left,
            _ => Vector3Int.zero
        };
    }

    private void HandleBonk()
    {
        if (bonkRoutine != null)
        {
            StopCoroutine(bonkRoutine);
            visuals.localPosition = lastBonkAnimStartPos;
        }

        bonkRoutine = StartCoroutine(BonkCo());
    }

    private IEnumerator BonkCo()
    {
        Vector3 startPos = visuals.localPosition;
        Vector3 targetOffset = startPos + (Vector3)FaceDirToVector(facingDirection) * bonkDistance;
        lastBonkAnimStartPos = startPos;

        while (Vector3.Distance(targetOffset, visuals.localPosition) > 0.01f)
        {
            float moveDistance = bonkAnimMoveSpeed * Time.deltaTime;
            visuals.localPosition = Vector3.MoveTowards(visuals.localPosition, targetOffset, moveDistance);
            yield return null;
        }

        yield return new WaitForSeconds(bonkAnimHoldTime);

        while (Vector3.Distance(visuals.localPosition, startPos) > 0.01f)
        {
            float moveDistance = bonkAnimMoveSpeed * Time.deltaTime;
            visuals.localPosition = Vector3.MoveTowards(visuals.localPosition, startPos, moveDistance);
            yield return null;
        }
    }
}
