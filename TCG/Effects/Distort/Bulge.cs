using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Bulge : IEffect
{
    public IntParameter X { get; set; } = new(0);
    public IntParameter Y { get; set; } = new(0);
    public FloatParameter Radius { get; set; } = new(50f) { Value = 50f };
    public FloatParameter Strenght { get; set; } = new(1f) { Value = 0.5f };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Bulge(X.Value, Y.Value, Radius.Value, Strenght.Value));
}
