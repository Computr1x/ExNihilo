using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class HSBCorrection : IEffect
{
    public SbyteParameter Hue { get; set; } = new(0) { Min = sbyte.MinValue, Max = sbyte.MaxValue};
    public SbyteParameter Saturation { get; set; } = new(0) { Min = sbyte.MinValue, Max = sbyte.MaxValue };
    public SbyteParameter Brightness { get; set; } = new(0) { Min = sbyte.MinValue, Max = sbyte.MaxValue };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.HSBCorrection(Hue, Saturation, Brightness));
}
