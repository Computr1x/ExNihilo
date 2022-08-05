using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Pixelate : IEffect
{
    public IntParameter PixelSize { get; set; } = new(4) { Min = 2, Max = 32 };

    public Pixelate() { }

    public Pixelate(int pixelSize)
    {
        PixelSize.Value = pixelSize;
    }

    public Pixelate WithPixelSize(int value)
    {
        PixelSize.Value = value;
        return this;
    }

    public Pixelate WithRandomizedPixelSize(int min, int max)
    {
        PixelSize.Min = min;
        PixelSize.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Pixelate(PixelSize));
}
