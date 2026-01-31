using SadUtils;
using System;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] KeybindsSO keybinds;
    public MaskHandler maskHandler;
    [SerializeField] Vector3Int facingDirection = Vector3Int.up;

    Vector3Int currentTile;

    public static Action<Vector3Int> OnPositionChanged;

    protected override void Awake()
    {
        SetInstance(this);
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
            if (Input.GetKeyDown(keybinds.Right))
            {
                TryMove(Vector3Int.right);
                facingDirection = Vector3Int.right;
            }
            if (Input.GetKeyDown(keybinds.Left))
            {
                TryMove(Vector3Int.left);
                facingDirection = Vector3Int.left;
            }
            if (Input.GetKeyDown(keybinds.Up))
            {
                TryMove(Vector3Int.up);
                facingDirection = Vector3Int.up;
            }
            if (Input.GetKeyDown(keybinds.Down))
            {
                TryMove(Vector3Int.down);
                facingDirection = Vector3Int.down;
            }

            TryUseMaskAbility();
        }
    }

    void TryMove(Vector3Int direction)
    {
        if (!MapManager.Instance.CheckIsWalkable(currentTile + direction)) { return; }
        if (MapManager.Instance.GetOccupiedTile(currentTile + direction) != null)
        {
            if (MapManager.Instance.GetOccupiedTile(currentTile + direction).walkable)
            {
                currentTile += direction;
                transform.position = currentTile;
                OnPositionChanged?.Invoke(currentTile);
                return;
            }
            if (maskHandler.GetActiveMask().Type != Mask.MaskType.Strength) return;
            if (MapManager.Instance.GetOccupiedTile(currentTile + direction).TryMove(direction, out bool KillSelf))
            {
                //MoveSelf
                currentTile += direction;
                transform.position = currentTile;
                OnPositionChanged?.Invoke(currentTile);
                return;
            }
        }
        else
        {
            currentTile += direction;
            transform.position = currentTile;
            OnPositionChanged?.Invoke(currentTile);
            return;
        }
    }

    void TryDash()
    {
        //Check if block between
        if (!MapManager.Instance.CheckIsWalkable(currentTile + facingDirection) && !MapManager.Instance.GetTileDataFromCell(currentTile + facingDirection).Skippable) return;

        //Check if block you try dash on is walkable from occupied space perspective
        if (MapManager.Instance.GetOccupiedTile(currentTile + (facingDirection)) != null && !MapManager.Instance.GetOccupiedTile(currentTile + (facingDirection)).walkable) return;
        if (MapManager.Instance.GetOccupiedTile(currentTile + (facingDirection * 2)) != null && !MapManager.Instance.GetOccupiedTile(currentTile + (facingDirection * 2)).walkable) return;

        //Check if block you try dash on is walkable
        if (MapManager.Instance.CheckIsWalkable(currentTile + (facingDirection * 2)))
        {
            currentTile += (facingDirection * 2);
            transform.position = currentTile;
            OnPositionChanged?.Invoke(currentTile);
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
                    }

                    break;
                default:
                    break;
            }
        }
    }
}
