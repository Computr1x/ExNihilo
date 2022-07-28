using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Scale : IEffect
{
    public FloatParameter XScale { get; set; } = new(0, 2);
    public FloatParameter YScale { get; set; } = new(0, 2);

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x =>
            x.Transform(new AffineTransformBuilder().AppendScale(new System.Numerics.Vector2(XScale.Value, YScale.Value))));
}
