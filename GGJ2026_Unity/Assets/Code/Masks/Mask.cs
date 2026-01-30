public abstract class Mask
{
    public enum MaskType
    {
        Empty,
        Strength,
        Dash
    }

    public enum UseState
    {
        InActive,
        Active,
        Cooldown,
        None
    }

    public abstract MaskType Type { get; }

    public abstract bool CanUse { get; }
    public abstract UseState MaskUseState { get; }

    public abstract void StartUse();
    public abstract void EndUse();
}
