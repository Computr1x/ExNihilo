using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Bulge : IEffect
{
    public IntParameter X { get; set; } = new(0);
    public IntParameter Y { get; set; } = new(0);
    public FloatParameter Radius { get; set; } = new(50f) { Min = 1, Max = 150};
    public FloatParameter Strenght { get; set; } = new(0.5f) { Min = 0, Max = 2f };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Bulge(X, Y, Radius, Strenght));
}
