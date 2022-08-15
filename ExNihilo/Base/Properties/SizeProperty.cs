using SixLabors.ImageSharp;

namespace ExNihilo.Base;

public class SizeProperty : Property
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

    public override void Randomize(Random r, bool force = false)
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
