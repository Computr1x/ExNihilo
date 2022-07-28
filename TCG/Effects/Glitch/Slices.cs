using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Slices : IEffect
{
    public IntParameter Seed { get; set; } = new(0);
    public IntParameter Count { get; set; } = new(0) { Value = 1};
    public IntParameter MinOffset { get; set; } = new(-15, 0) { Value = -10 };
    public IntParameter MaxOffset { get; set; } = new(0, 15) { Value = 10 };
    public IntParameter SliceHeight { get; set; } = new(1, 10) { Value = 10 };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Slices(Seed.Value, Count.Value, SliceHeight.Value, MinOffset.Value, MaxOffset.Value));
}
