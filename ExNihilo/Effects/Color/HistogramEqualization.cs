using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines extension that allow the adjustment of the contrast of an image via its histogram.
/// </summary>
public class HistogramEqualization : Effect
{
    public override EffectType EffectType => EffectType.Color;
    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.HistogramEqualization());
}
