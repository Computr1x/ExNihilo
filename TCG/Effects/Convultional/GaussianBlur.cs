using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class GaussianBlur : IEffect
    {
        public float Sigma { get; set; } = 0.5f;

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.GaussianBlur(Sigma));
    }
}
