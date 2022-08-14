using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;

namespace ExNihilo.Effects;

/// <summary>
/// Define the effect thar allow to alter colors of the <see cref="IDrawable"/> recreating old Kodachrome camera effect
/// </summary>
public class KodaChrome : IEffect
{
    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Kodachrome());
}
