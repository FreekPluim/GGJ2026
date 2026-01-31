public class EmptyMask : Mask
{
    public override MaskType Type => MaskType.Empty;

    public override bool CanUse => false;
    public override UseState MaskUseState => UseState.None;

    public EmptyMask(MaskSO data) : base(data) { }

    public override void EndUse() { }

    public override void StartUse() { }
}
