public class DashMask : Mask
{
    public override MaskType Type => MaskType.Dash;

    public override bool CanUse => true;
    public override UseState MaskUseState => UseState.Active;

    public DashMask(MaskSO data) : base(data) { }

    public override void EndUse()
    {

    }

    public override void StartUse()
    {

    }
}
