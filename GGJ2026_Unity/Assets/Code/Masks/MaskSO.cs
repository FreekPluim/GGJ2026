using UnityEngine;

public abstract class MaskSO : ScriptableObject
{
    public string title;
    public GameObject uiVisualPrefab;
    public GameObject pickupVisualPrefab;
    public string description;

    public abstract Mask GenerateMask();
}
