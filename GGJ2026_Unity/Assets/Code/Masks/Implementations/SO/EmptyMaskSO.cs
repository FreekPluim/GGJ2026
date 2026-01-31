using UnityEngine;

[CreateAssetMenu(fileName = "EmptyMask", menuName = "SO/Mask/Empty Mask")]
public class EmptyMaskSO : MaskSO
{
    public override Mask GenerateMask()
    {
        Mask emptyMask = new EmptyMask(this);
        return emptyMask;
    }
}
