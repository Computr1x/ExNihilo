using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of quantinization on an <see cref="Visual"/>
/// </summary>
public class Quantize : Effect
{
    public override EffectType EffectType => EffectType.Convultional;
    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Quantize());
}