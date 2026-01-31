using UnityEngine;

public class MaskPickup : Obstacle
{
    [SerializeField] private KeybindsSO keybinds;
    [SerializeField] private MaskSO startMask;
    [SerializeField] private Transform visualHolder;

    private Mask currentMask;

    private bool canPickup;

    private void Awake()
    {
        currentMask = startMask.GenerateMask();
        GenerateVisuals();

        PlayerController.OnPositionChanged += PlayerController_OnPositionChanged;
    }

    private void OnDestroy()
    {
        PlayerController.OnPositionChanged -= PlayerController_OnPositionChanged;
    }

    private void PlayerController_OnPositionChanged(Vector3Int newPosition)
    {
        canPickup = newPosition == gridPosition;
    }

    private void Update()
    {
        if (!canPickup || !Input.GetKeyDown(keybinds.pickup))
        {
            return;
        }

        PickupMask();
    }

    private void PickupMask()
    {
        PlayerController.Instance.maskHandler.PickupMask(currentMask, out Mask droppedMask);
        if (droppedMask == null)
        {
            Destroy(gameObject);
            return;
        }

        currentMask = droppedMask;
        GenerateVisuals();
    }

    private void GenerateVisuals()
    {
        if (visualHolder.childCount > 0)
        {
            Destroy(visualHolder.GetChild(0).gameObject);
        }

        Instantiate(currentMask.pickupVisualPrefab, visualHolder);
    }
}
