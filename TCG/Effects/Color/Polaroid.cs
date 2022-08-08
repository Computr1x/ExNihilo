using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of polaroid effect to the <see cref="IDrawable"/>
/// </summary>
public class Polaroid : IEffect
{
    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Polaroid());
}
