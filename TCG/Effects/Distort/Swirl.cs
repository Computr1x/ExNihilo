using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Swirl : IEffect
{
    public IntParameter X { get; set; } = new(0);
    public IntParameter Y { get; set; } = new(0);
    // radius of effect in pixels
    public FloatParameter Radius { get; set; } = new(100f) { Value = 100f };
    public FloatParameter Degree { get; set; } = new(360f) { Value = 10f };
    public FloatParameter Twists { get; set; } = new(3f) { Value = 1f };

    public void Render(Image image, GraphicsOptions graphicsOptions)
    {
        if (X.Value == 0 && Y.Value == 0)
            image.Mutate(x => x.Swirl(Radius.Value, Degree.Value, Twists.Value));
        else
            image.Mutate(x => x.Swirl(X.Value, Y.Value, Radius.Value, Degree.Value, Twists.Value));
    }
}
