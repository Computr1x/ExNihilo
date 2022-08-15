using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using ExNihilo.Base.Abstract;
using ExNihilo.Base.Utils;

namespace ExNihilo.Base.Parameters;

public class PenParameter : ComplexParameter
{
    public EnumParameter<PenType> Type { get; } = new EnumParameter<PenType>(PenType.Solid);
    public IntParameter Width { get; } = new IntParameter(1) { Min = 1, Max = 10 };
    public ColorParameter Color { get; } = new ColorParameter(SixLabors.ImageSharp.Color.Black, 10);

    public PenParameter WithValue(PenType type, int width, Color color)
    {
        Type.WithValue(type);
        Width.WithValue(width);
        Color.WithValue(color);
        return this;
    }

    public PenParameter WithType(PenType type)
    {
        Type.Value = type;
        return this;
    }

    public PenParameter WithRandomizedType()
    {
        Type.EnumValues = (PenType[]) Enum.GetValues(typeof(PenType));
        return this;
    }

    public PenParameter WithRandomizedType(IEnumerable<PenType> types)
    {
        Type.EnumValues = types.ToArray();
        return this;
    }

    public PenParameter WithWidth(int value)
    {
        Width.Value = value;
        return this;
    }

    public PenParameter WithRandomizedWidth(int min, int max)
    {
        Width.Min = min;
        Width.Max = max;
        return this;
    }

    public PenParameter WithColor(Color color)
    {
        Color.Value = color;
        return this;
    }

    public PenParameter WithRandomizedColor(int colorsCount, byte opacity = 255)
    {
        Color.Opacity = opacity;
        Color.Colors = Color.GeneratePalette(colorsCount);
        return this;
    }

    public PenParameter WithRandomizedColor(Color[] palette)
    {
        Color.Colors = palette;
        return this;
    }

    protected override void RandomizeImplementation(Random r)
    {
        Type.Randomize(r);
        Width.Randomize(r);
        Color.Randomize(r);
    }

    public IPen GetValue()
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
    public IPen Value { get => GetValue(); }
}
