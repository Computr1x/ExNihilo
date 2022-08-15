using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the recreating old Lomograph camera effect to the <see cref="Visual"/>
/// </summary>
public class Lomograph : Effect
{
    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Lomograph());
}