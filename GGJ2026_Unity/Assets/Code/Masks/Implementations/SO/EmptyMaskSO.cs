using UnityEngine;

[CreateAssetMenu(fileName = "EmptyMask", menuName = "SO/Mask/Empty Mask")]
public class EmptyMaskSO : MaskSO
{
    public override Mask GenerateMask()
    {
        return new EmptyMask();
    }
}
