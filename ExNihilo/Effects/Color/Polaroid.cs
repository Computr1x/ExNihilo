using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of polaroid effect to the <see cref="Visual"/>
/// </summary>
public class Polaroid : Effect
{
    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Polaroid());
}
