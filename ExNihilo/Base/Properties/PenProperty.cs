using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;

namespace ExNihilo.Base;

public enum PenType
{
    Solid,
    Dot,
    Dash,
    DashDot,
    DashDotDot
}

public class PenProperty : Property
{
    public EnumProperty<PenType> Type { get; } = new EnumProperty<PenType>(PenType.Solid);
    public IntProperty Width { get; } = new IntProperty(1) { Min = 1, Max = 10 };
    public ColorProperty Color { get; } = new ColorProperty(SixLabors.ImageSharp.Color.Black, 10);

    public PenProperty WithValue(PenType type, int width, Color color)
    {
        Type.WithValue(type);
        Width.WithValue(width);
        Color.WithValue(color);
        return this;
    }

    public PenProperty WithType(PenType type)
    {
        Type.Value = type;
        return this;
    }

    public PenProperty WithRandomizedType()
    {
        Type.EnumValues = (PenType[]) Enum.GetValues(typeof(PenType));
        return this;
    }

    public PenProperty WithRandomizedType(IEnumerable<PenType> types)
    {
        Type.EnumValues = types.ToArray();
        return this;
    }

    public PenProperty WithWidth(int value)
    {
        Width.Value = value;
        return this;
    }

    public PenProperty WithRandomizedWidth(int min, int max)
    {
        Width.Min = min;
        Width.Max = max;
        return this;
    }

    public PenProperty WithColor(Color color)
    {
        Color.Value = color;
        return this;
    }

    public PenProperty WithRandomizedColor(int colorsCount, byte opacity = 255)
    {
        Color.Opacity = opacity;
        Color.Colors = Color.GeneratePalette(colorsCount);
        return this;
    }

    public PenProperty WithRandomizedColor(Color[] palette)
    {
        Color.Colors = palette;
        return this;
    }

    public override void Randomize(Random r, bool force = false)
    {
        Type.Randomize(r);
        Width.Randomize(r);
        Color.Randomize(r);
    }

    public Pen GetValue()
    {
        return Type.Value switch
        {
            PenType.Dot => Pens.Dot(Color, Width),
            PenType.Dash => Pens.Dash(Color, Width),
            PenType.DashDot => Pens.DashDot(Color, Width),
            PenType.DashDotDot => Pens.DashDotDot(Color, Width),
            _ => Pens.Solid(Color, Width)
        };
    }
    public Pen Value { get => GetValue(); }
}
