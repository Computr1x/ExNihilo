using SixLabors.ImageSharp;
using TCG.Base.Abstract;

namespace TCG.Base.Parameters;

public class SizeParameter : ComplexParameter
{
    public IntParameter Width { get; set; } = new IntParameter(0);
    public IntParameter Height { get; set; } = new IntParameter(0);

    public SizeParameter WithValue(Size value)
    {
        Width.WithValue(value.Width);
        Height.WithValue(value.Height);
        return this;
    }

    public SizeParameter WithRandomizedValue(int minWidth, int maxWidth, int minHeught, int maxHeight)
    {
        Width.WithRandomizedValue(minWidth, maxWidth);
        Height.WithRandomizedValue(minHeught, maxHeight);
        return this;
    }

    protected override void RandomizeImplementation(Random r)
    {
        Width.Randomize(r);
        Height.Randomize(r);
    }

    public static implicit operator Size(SizeParameter sizeParameter)
    {
        return new Size(sizeParameter.Width, sizeParameter.Height);
    }

    public static implicit operator SizeF(SizeParameter sizeParameter)
    {
        return new SizeF(sizeParameter.Width, sizeParameter.Height);
    }
}
