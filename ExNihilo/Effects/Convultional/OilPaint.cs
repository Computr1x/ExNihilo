using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of oil painting effect on an <see cref="Visual"/>
/// </summary>
public class OilPaint : Effect
{
    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.OilPaint());
}