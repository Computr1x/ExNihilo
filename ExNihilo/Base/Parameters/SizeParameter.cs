using SixLabors.ImageSharp;
using ExNihilo.Base.Abstract;

namespace ExNihilo.Base.Properties;

public class SizeProperty : ComplexProperty
{
    public IntProperty Width { get; set; } = new IntProperty(0);
    public IntProperty Height { get; set; } = new IntProperty(0);

    public SizeProperty WithValue(Size value)
    {
        Width.WithValue(value.Width);
        Height.WithValue(value.Height);
        return this;
    }

    public SizeProperty WithRandomizedValue(int minWidth, int maxWidth, int minHeught, int maxHeight)
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

    public static implicit operator Size(SizeProperty sizeProperty)
    {
        return new Size(sizeProperty.Width, sizeProperty.Height);
    }

    public static implicit operator SizeF(SizeProperty sizeProperty)
    {
        return new SizeF(sizeProperty.Width, sizeProperty.Height);
    }
}
