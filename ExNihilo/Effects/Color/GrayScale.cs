using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of grayscale toning of the <see cref="Visual"/>
/// </summary>
public class GrayScale : Effect
{
    public override EffectType EffectType => EffectType.Color;
    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Grayscale());
}
