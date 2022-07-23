using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;

namespace TCG.Effects
{
    public class Slices : IEffect
    {
        public int Seed { get; set; } = 0;
        public int Count { get; set; } = 1;
        public int MinOffset { get; set; } = -10;
        public int MaxOffset { get; set; } = 20;

        public int SliceHeight { get; set; } = 4;

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.Slices(Seed, Count, SliceHeight, MinOffset, MaxOffset));
    }
}
