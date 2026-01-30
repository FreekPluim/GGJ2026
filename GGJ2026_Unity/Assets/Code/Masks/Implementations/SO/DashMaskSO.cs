using UnityEngine;

[CreateAssetMenu(fileName = "DashMask", menuName = "SO/Mask/Dash Mask")]
public class DashMaskSO : MaskSO
{
    public override Mask GenerateMask()
    {
        return new DashMask();
    }
}
