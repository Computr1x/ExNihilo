using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class SlitScan : IEffect
{
    public FloatParameter Time { get; set; } = new(10f) { Value = 2f };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.SlitScan(Time.Value));
}
