using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Shift : IEffect
{
    public FloatParameter XShift { get; set; } = new(0) { Min = 0, Max = 50 };
    public FloatParameter YShift { get; set; } = new(0) { Min = 0, Max = 50 };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Transform(new AffineTransformBuilder().AppendTranslation(new System.Numerics.Vector2(XShift, YShift))));
}
