using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Shift : IEffect
{
    public FloatParameter XShift { get; set; } = new(0, 2);
    public FloatParameter YShift { get; set; } = new(0, 2);

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Transform(new AffineTransformBuilder().AppendTranslation(new System.Numerics.Vector2(XShift.Value, YShift.Value))));
}
