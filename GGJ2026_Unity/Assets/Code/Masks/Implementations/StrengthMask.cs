public class StrengthMask : Mask
{
    public override MaskType Type => MaskType.Strength;

    public override bool CanUse => true;
    public override UseState MaskUseState => UseState.Active;

    public override void EndUse()
    {

    }

    public override void StartUse()
    {

    }
}
