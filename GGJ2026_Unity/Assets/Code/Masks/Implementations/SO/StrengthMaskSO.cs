using UnityEngine;

[CreateAssetMenu(fileName = "StrengthMask", menuName = "SO/Mask/Strength Mask")]
public class StrengthMaskSO : MaskSO
{
    public override Mask GenerateMask()
    {
        Mask strengthMask = new StrengthMask(this);
        return strengthMask;
    }
}
