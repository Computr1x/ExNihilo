using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class Crop : IEffect
    {
        public Rectangle Rectangle { get; set; }

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.Crop(Rectangle));
    }
}