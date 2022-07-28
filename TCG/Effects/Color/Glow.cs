using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Glow : IEffect
{
    public FloatParameter Radius { get; set; } = new FloatParameter(15) { Min = 1, Max = 150 };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Glow(Radius));
}
