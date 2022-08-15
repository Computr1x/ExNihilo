using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of black an white toning of the <see cref="Visual"/>
/// </summary>
public class BlackWhite : Effect
{
    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.BlackWhite());
}