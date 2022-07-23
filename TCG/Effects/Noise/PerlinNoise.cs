using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;

namespace TCG.Effects
{
    public class PerlinNoise : IEffect
    {
        public int Seed { get; set; } = 0;

        public bool Monochrome { get; set; } = false;

        public int Octaves { get; set; } = 5;

        public float Persistence { get; set; } = 0.5f;

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.PerlinNoise(Seed, Octaves, Persistence, Monochrome));
    }
}
