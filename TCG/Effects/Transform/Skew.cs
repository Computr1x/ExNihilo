using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Skew : IEffect
{
    public FloatParameter XDegrees { get; set; } = new(0, 360);
    public FloatParameter YDegrees { get; set; } = new(0, 360);

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x =>
            x.Transform(new AffineTransformBuilder().AppendSkewDegrees(XDegrees.Value, YDegrees.Value)));
}
