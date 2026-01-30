using UnityEngine;

[CreateAssetMenu(fileName = "StrengthMask", menuName = "SO/Mask/Strength Mask")]
public class StrengthMaskSO : MaskSO
{
    public override Mask GenerateMask()
    {
        return new StrengthMask();
    }
}
