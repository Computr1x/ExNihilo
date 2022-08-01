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
