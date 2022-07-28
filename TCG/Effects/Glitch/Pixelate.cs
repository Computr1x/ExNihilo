using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Pixelate : IEffect
{
    public IntParameter PixelSize { get; set; } = new(16) { Value = 3 };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Pixelate(PixelSize.Value));
}
