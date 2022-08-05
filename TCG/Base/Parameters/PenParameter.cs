using SixLabors.ImageSharp.Drawing.Processing;
using TCG.Base.Abstract;
using TCG.Base.Utils;
using TCG.Base.Parameters;

namespace TCG.Base.Parameters;

public class PenParameter : GenericParameter<IPen>
{
    public EnumParameter<PenType> Type { get; set; } = new EnumParameter<PenType>(PenType.Solid);
    public IntParameter Width { get; set; } = new IntParameter(1) { Min = 1, Max = 10 };
    public ColorParameter Color { get; set; } = new ColorParameter(SixLabors.ImageSharp.Color.Black, 10);

    public PenParameter(IPen defaultValue) : base(defaultValue) { }

    public PenParameter WithType(PenType type)
    {
        Type.Value = type;
        return this;
    }

    public PenParameter WithRandomizedType()
    {
        Type.EnumValues = (PenType[])Enum.GetValues(typeof(PenType));
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

    public PenParameter WithColor(SixLabors.ImageSharp.Color color)
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

    public PenParameter WithRandomizedColor(SixLabors.ImageSharp.Color[] palette)
    {
        Color.Colors = palette;
        return this;
    }

    

    protected override void RandomizeImplementation(Random r)
    {  
        Type.Randomize(r);
        Width.Randomize(r);
        Color.Randomize(r);
        
        Value = Type.Value switch
        {
            PenType.Dot => Pens.Dot(Color, Width),
            PenType.Dash => Pens.Dash(Color, Width),
            PenType.DashDot => Pens.DashDot(Color, Width),
            PenType.DashDotDot => Pens.DashDotDot(Color, Width),
            _ => Pens.Solid(Color, Width)
        };
    }
}
