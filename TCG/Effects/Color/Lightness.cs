using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class Lightness : IEffect
    {
        public float Amount { get; set; } = 1f;

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.Lightness(Amount));
    }
}