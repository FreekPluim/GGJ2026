using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MaskContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
    [SerializeField] private GameObject tooltipObject;
    [SerializeField] private TMP_Text maskDescriptionLabel;

    private Mask maskData;

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
        if (newMaskData == null)
        {
            maskLabel.text = "-Empty-";

            if (maskVisualHolder.transform.childCount > 0)
            {
                Destroy(maskVisualHolder.transform.GetChild(0).gameObject);
            }

            if (tooltipObject.activeSelf)
            {
                tooltipObject.SetActive(false);
            }
            return;
        }

        maskLabel.text = newMaskData.title;
        maskDescriptionLabel.text = newMaskData.description;
        maskData = newMaskData;

        if (maskVisualHolder.transform.childCount > 0)
        {
            Destroy(maskVisualHolder.transform.GetChild(0).gameObject);
        }

        Instantiate(newMaskData.uiVisualPrefab, maskVisualHolder.transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (maskData == null)
        {
            return;
        }

        tooltipObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipObject.SetActive(false);
    }
}
