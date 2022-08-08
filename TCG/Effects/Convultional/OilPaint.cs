using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of oil painting effect on an <see cref="IDrawable"/>
/// </summary>
public class OilPaint : IEffect
{
    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.OilPaint());
}