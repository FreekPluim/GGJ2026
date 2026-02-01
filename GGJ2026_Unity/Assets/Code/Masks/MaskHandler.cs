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
    public static Action<SwapMaskData> OnMasksChanged;

    public struct SwapMaskData
    {
        public Mask newActiveMask;
        public Mask newInventoryMask;
    }

    private void Start()
    {
        if (startMask == null)
        {
            Debug.LogError("CATASTROPHIC FAILURE LMAO\nI DIE NOW GOODBYE THANK");
            return;
        }

        if (startInventoryMask != null)
        {
            inventoryMask = startInventoryMask.GenerateMask();
        }

        activeMask = startMask.GenerateMask();
        activeMask.StartUse();

        OnMasksChanged?.Invoke(new SwapMaskData
        {
            newActiveMask = activeMask,
            newInventoryMask = inventoryMask
        });
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
        if (inventoryMask == null)
        {
            // Inventory is empty, can't swap!
            return;
        }

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

    public void PickupMask(Mask mask, out Mask droppedMask)
    {
        droppedMask = null;
        if (AudioManager.instance != null) AudioManager.instance.PlayOneShot("OnPickup");

        if (inventoryMask == null)
        {
            inventoryMask = mask;
            SwapMask();
            return;
        }

        droppedMask = activeMask;
        activeMask.EndUse();

        activeMask = mask;
        activeMask.StartUse();


        OnMasksChanged?.Invoke(new SwapMaskData
        {
            newActiveMask = activeMask,
            newInventoryMask = inventoryMask
        });
    }
}
