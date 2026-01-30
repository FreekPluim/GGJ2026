using TMPro;
using UnityEngine;

public class MaskContainer : MonoBehaviour
{
    public enum MaskListenerType
    {
        Active,
        Inventory
    }

    [Header("Settings")]
    [SerializeField] private MaskListenerType listenerType;

    [Header("References")]
    [SerializeField] private TMP_Text maskLabel;
    [SerializeField] private GameObject maskVisualHolder;

    private void Awake()
    {
        MaskHandler.OnMasksChanged += MaskHandler_OnMasksChanged;
    }

    private void OnDestroy()
    {
        MaskHandler.OnMasksChanged -= MaskHandler_OnMasksChanged;
    }

    private void MaskHandler_OnMasksChanged(MaskHandler.SwapMaskData data)
    {
        Mask newMaskData = listenerType == MaskListenerType.Active ? data.newActiveMask : data.newInventoryMask;

        maskLabel.text = newMaskData.title;

        if (maskVisualHolder.transform.childCount > 0)
        {
            Destroy(maskVisualHolder.transform.GetChild(0).gameObject);
        }

        Instantiate(newMaskData.visualPrefab, maskVisualHolder.transform);
    }
}
