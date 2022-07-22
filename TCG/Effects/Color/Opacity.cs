using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class Opacity : IEffect
    {
        // beteween 0 - 1
        public float Amount { get; set; } = 0.5f;

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.Opacity(Amount));
    }
}
