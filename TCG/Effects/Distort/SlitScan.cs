using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;

namespace TCG.Effects
{
    public class SlitScan : IEffect
    {
        public float Time { get; set; } = 3f;

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.SlitScan(Time));
    }
}
