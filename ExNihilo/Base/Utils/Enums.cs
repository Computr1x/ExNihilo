namespace ExNihilo.Base.Utils;

public enum PenType 
{ 
    Solid, 
    Dot, 
    Dash,
    DashDot,
    DashDotDot 
}

public enum BrushType
{
    Solid, 
    Vertical, 
    Horizontal, 
    BackwardDiagonal, 
    ForwardDiagonal, 
    Min, 
    Percent10,
    Percent20
}

[Flags]
public enum VisualType : byte {
    Filled = 1 << 0,
    Outlined = 1 << 1,
    FillWithOutline = Filled | Outlined
}