using SixLabors.ImageSharp;
using TCG.Base.Abstract;

namespace TCG.Rnd.Randomizers.Parameters;

public class SizeParameter : GenericStructParameter<Size>
{
    public IntParameter Width { get; set; } = new IntParameter(0);
    public IntParameter Height { get; set; } = new IntParameter(0);

    public SizeParameter(Size defaultValue) : base(defaultValue)
    {
    }

    protected override void RandomizeImplementation(Random r)
    {
        Width.Randomize(r);
        Height.Randomize(r);

        Value = new(Width, Height);
    }

    public static implicit operator SizeF(SizeParameter sizeParameter)
    {
        Size size = sizeParameter;
        return new SizeF(size.Width, size.Height);
    }
}
