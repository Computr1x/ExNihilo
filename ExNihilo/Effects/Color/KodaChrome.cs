using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;

namespace ExNihilo.Effects;

/// <summary>
/// Define the effect thar allow to alter colors of the <see cref="Visual"/> recreating old Kodachrome camera effect
/// </summary>
public class KodaChrome : Effect
{
    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Kodachrome());
}
