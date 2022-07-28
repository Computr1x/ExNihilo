using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Pixelate : IEffect
{
    public IntParameter PixelSize { get; set; } = new(4) { Min = 2, Max = 32 };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Pixelate(PixelSize));
}
