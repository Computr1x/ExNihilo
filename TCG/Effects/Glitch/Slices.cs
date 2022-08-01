using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Slices : IEffect
{
    public IntParameter Seed { get; set; } = new(0) { Min = 0, Max = int.MaxValue };
    public IntParameter Count { get; set; } = new(10) { Min = 1, Max = 10 };
    public IntParameter MinOffset { get; set; } = new() { Min = -10, Max = 0 };
    public IntParameter MaxOffset { get; set; } = new() { Min = 0, Max = 10 };
    public IntParameter SliceHeight { get; set; } = new(1) { Min = 1, Max = 10 };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Slices(Seed, Count, SliceHeight, MinOffset, MaxOffset));
}
