using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;

namespace ExNihilo.Effects;

/// <summary>
/// Invert colors of the <see cref="Visual"/>
/// </summary>
public class Invert : Effect
{
    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Invert());
}
