using UnityEngine;

[CreateAssetMenu(fileName = "StrengthMask", menuName = "SO/Mask/Strength Mask")]
public class StrengthMaskSO : MaskSO
{
    public override Mask GenerateMask()
    {
        Mask strengthMask = new StrengthMask();

        strengthMask.title = title;
        strengthMask.visualPrefab = visualPrefab;

        return strengthMask;
    }
}
