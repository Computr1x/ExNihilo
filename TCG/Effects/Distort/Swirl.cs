using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Swirl : IEffect
{
    public IntParameter X { get; set; } = new(0);
    public IntParameter Y { get; set; } = new(0);
    // radius of effect in pixels
    public FloatParameter Radius { get; set; } = new(100f) { Min = 1f, Max = 150f };
    public FloatParameter Degree { get; set; } = new(10f) { Min = 0f, Max = 360f };
    public FloatParameter Twists { get; set; } = new(0.5f) { Min = 0f, Max = 3f };

    public Swirl() { }

    public Swirl(float radius, float degree, float twists)
    {
        Radius.Value = radius;
        Degree.Value = degree;
        Twists.Value = twists;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions)
    {
        if (!X.Value.HasValue && !Y.Value.HasValue)
            image.Mutate(x => x.Swirl(Radius, Degree, Twists));
        else
            image.Mutate(x => { x.SetGraphicsOptions(graphicsOptions); x.Swirl(X, Y, Radius, Degree, Twists); });
    }
}
