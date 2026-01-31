using UnityEngine;

public abstract class Mask
{
    public enum MaskType
    {
        Empty,
        Strength,
        Dash
    }

    public enum UseState
    {
        InActive,
        Active,
        Cooldown,
        None
    }

    public abstract MaskType Type { get; }

    public abstract bool CanUse { get; }
    public abstract UseState MaskUseState { get; }

    public string title;
    public GameObject pickupVisualPrefab;
    public GameObject uiVisualPrefab;
    public string description;

    public Mask(MaskSO data)
    {
        title = data.title;
        pickupVisualPrefab = data.pickupVisualPrefab;
        uiVisualPrefab = data.uiVisualPrefab;
        description = data.description;
    }

    public abstract void StartUse();
    public abstract void EndUse();
}
