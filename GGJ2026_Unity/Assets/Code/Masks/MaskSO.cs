using UnityEngine;

public abstract class MaskSO : ScriptableObject
{
    public string title;
    public GameObject visualPrefab;

    public abstract Mask GenerateMask();
}
