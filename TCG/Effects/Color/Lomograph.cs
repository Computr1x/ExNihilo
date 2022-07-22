using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class Lomograph : IEffect
    {
        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.Lomograph());
    }
}