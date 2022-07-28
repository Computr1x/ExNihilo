using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class HSBCorrection : IEffect
{
    public SbyteParameter Hue { get; set; } = new(sbyte.MinValue, sbyte.MaxValue);
    public SbyteParameter Saturation { get; set; } = new(sbyte.MinValue, sbyte.MaxValue);
    public SbyteParameter Brightness { get; set; } = new(sbyte.MinValue, sbyte.MaxValue);

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.HSBCorrection(Hue.Value, Saturation.Value, Brightness.Value));
}
