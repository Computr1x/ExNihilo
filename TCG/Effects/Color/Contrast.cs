using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class Contrast : IEffect
    {
        public float Amount { get; set; } = 3f;

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.Contrast(Amount));
    }
}