using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Rotate : IEffect
{
    public FloatParameter Degrees { get; set; } = new(0) { Min = 0, Max = 360 };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Rotate(Degrees));
}
