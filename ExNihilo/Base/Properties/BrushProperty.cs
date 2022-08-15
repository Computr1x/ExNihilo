using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;

namespace ExNihilo.Base;

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

public class BrushProperty : Property
{
    public EnumProperty<BrushType> Type { get; set; } = new EnumProperty<BrushType>(BrushType.Solid);
    public ColorProperty Color { get; set; } = new ColorProperty(SixLabors.ImageSharp.Color.Black);

    public BrushProperty WithValue(BrushType type, Color color)
    {
        Type.WithValue(type);
        Color.WithValue(color);
        return this;
    }

    public BrushProperty WithType(BrushType type)
    {
        Type.Value = type;
        return this;
    }

    public BrushProperty WithRandomizedType()
    {
        Type.EnumValues = (BrushType[]) Enum.GetValues(typeof(BrushType));
        return this;
    }

    public BrushProperty WithRandomizedType(IEnumerable<BrushType> types)
    {
        Type.EnumValues = types.ToArray();
        return this;
    }

    public BrushProperty WithColor(Color color)
    {
        Color.Value = color;
        return this;
    }

    public BrushProperty WithRandomizedColor(int colorsCount, byte opacity = 255)
    {
        Color.Opacity = opacity;
        Color.Colors = Color.GeneratePalette(colorsCount);
        return this;
    }

    public BrushProperty WithRandomizedColor(Color[] palette)
    {
        Color.Colors = palette;
        return this;
    }

    private IBrush GetBrush() => Type.Value switch
    {
        BrushType.Vertical => Brushes.Vertical(Color),
        BrushType.Horizontal => Brushes.Horizontal(Color),
        BrushType.BackwardDiagonal => Brushes.BackwardDiagonal(Color),
        BrushType.ForwardDiagonal => Brushes.ForwardDiagonal(Color),
        BrushType.Min => Brushes.Min(Color),
        BrushType.Percent10 => Brushes.Percent10(Color),
        BrushType.Percent20 => Brushes.Percent20(Color),
        _ => Brushes.Solid(Color)
    };

    public override void Randomize(Random r, bool force = false)
    {
        Type.Randomize(r);
        Color.Randomize(r);
    }

    public IBrush Value { get => GetValue(); }

    public IBrush GetValue() => Type.Value switch
    {
        BrushType.Vertical => Brushes.Vertical(Color),
        BrushType.Horizontal => Brushes.Horizontal(Color),
        BrushType.BackwardDiagonal => Brushes.BackwardDiagonal(Color),
        BrushType.ForwardDiagonal => Brushes.ForwardDiagonal(Color),
        BrushType.Min => Brushes.Min(Color),
        BrushType.Percent10 => Brushes.Percent10(Color),
        BrushType.Percent20 => Brushes.Percent20(Color),
        _ => Brushes.Solid(Color)
    };

    //public static implicit operator IBrush(BrushProperty brushProperty)
    //{
    //    return brushProperty.Type.Value switch
    //    {
    //        BrushType.Vertical => Brushes.Vertical(brushProperty.Color),
    //        BrushType.Horizontal => Brushes.Horizontal(brushProperty.Color),
    //        BrushType.BackwardDiagonal => Brushes.BackwardDiagonal(brushProperty.Color),
    //        BrushType.ForwardDiagonal => Brushes.ForwardDiagonal(brushProperty.Color),
    //        BrushType.Min => Brushes.Min(brushProperty.Color),
    //        BrushType.Percent10 => Brushes.Percent10(brushProperty.Color),
    //        BrushType.Percent20 => Brushes.Percent20(brushProperty.Color),
    //        _ => Brushes.Solid(brushProperty.Color)
    //    };
    //}
}
