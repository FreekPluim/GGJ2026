using System;
using UnityEngine;

public class MaskHandler : MonoBehaviour
{
    [Header("Starting Inventory")]
    [SerializeField] private MaskSO startMask;
    [SerializeField] private MaskSO startInventoryMask;

    [Header("Settings")]
    [SerializeField] private KeybindsSO keybinds;

    private Mask activeMask;
    private Mask inventoryMask;

    // events
    public Action<SwapMaskData> OnMasksChanged;

    public struct SwapMaskData
    {
        public Mask newActiveMask;
        public Mask newInventoryMask;
    }

    private void Start()
    {
        activeMask = startMask.GenerateMask();
        inventoryMask = startInventoryMask.GenerateMask();

        activeMask.StartUse();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keybinds.swapMaskBind))
        {
            SwapMask();
        }
    }

    private void SwapMask()
    {
        (activeMask, inventoryMask) = (inventoryMask, activeMask);

        inventoryMask.EndUse();
        activeMask.StartUse();

        OnMasksChanged?.Invoke(new SwapMaskData
        {
            newActiveMask = activeMask,
            newInventoryMask = inventoryMask
        });
    }

    public Mask GetActiveMask()
    {
        return activeMask;
    }

    public Mask GetInventoryMask()
    {
        return inventoryMask;
    }
}
