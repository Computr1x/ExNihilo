using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Pad : IEffect
{
    public IntParameter Width { get; set; } = new(0) { Min = 0, Max = 50 };
    public IntParameter Height { get; set; } = new(0) { Min = 0, Max = 50 };

    public Pad() { }

    public Pad(int width, int height) { 
        Width.Value = width;
        Height.Value = height;
    }

    public Pad WithWidth(int value)
    {
        Width.Value = value;
        return this;
    }

    public Pad WithRandomizedWidth(int min, int max)
    {
        Width.Min = min;
        Width.Max = max;
        return this;
    }

    public Pad WithHeight(int value)
    {
        Height.Value = value;
        return this;
    }

    public Pad WithRandomizedHeight(int min, int max)
    {
        Height.Min = min;
        Height.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Pad(Width, Height));
}
