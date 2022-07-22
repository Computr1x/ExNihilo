using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class Flip : IEffect
    {
        public FlipMode FlipMode { get; set; } = FlipMode.Horizontal;

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.Flip(FlipMode));
    }
}
