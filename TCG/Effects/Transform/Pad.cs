using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class Pad : IEffect
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.Pad(Width, Height));
    }
}
