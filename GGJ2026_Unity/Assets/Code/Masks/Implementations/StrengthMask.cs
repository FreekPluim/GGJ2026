public class StrengthMask : Mask
{
    public override MaskType Type => MaskType.Strength;

    public override bool CanUse => true;
    public override UseState MaskUseState => UseState.Active;

    public StrengthMask(MaskSO data) : base(data) { }

    public override void EndUse()
    {

    }

    public override void StartUse()
    {

    }
}
