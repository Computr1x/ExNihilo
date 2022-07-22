using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class Pixelate : IEffect
    {
        public int PixelSize { get; set; } = 3;

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.Pixelate(PixelSize));
    }
}
