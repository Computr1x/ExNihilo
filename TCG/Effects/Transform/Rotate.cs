using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Rotate : IEffect
{
    public FloatParameter Degrees { get; set; } = new(0, 360);

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Rotate(Degrees.Value));
}
