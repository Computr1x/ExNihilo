using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the recreating old Lomograph camera effect to the <see cref="IDrawable"/>
/// </summary>
public class Lomograph : IEffect
{
    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Lomograph());
}