using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Ripple : IEffect
{
    public IntParameter X { get; set; } = new(0);
    public IntParameter Y { get; set; } = new(0);
    // radius of effect in pixels
    public FloatParameter Radius { get; set; } = new(100f) { Min = 1f, Max = 100f };
    //  wavelength of ripples, in pixels
    public FloatParameter WaveLength { get; set; } = new FloatParameter(10f) { Min = 1f, Max = 10f };
    // approximate width of wave train, in wavelengths
    public FloatParameter TraintWidth { get; set; } = new(2) { Min = 1f, Max = 10f };

    public void Render(Image image, GraphicsOptions graphicsOptions)
    {
        if (!X.Value.HasValue && !Y.Value.HasValue)
            image.Mutate(x => x.Ripple(Radius, WaveLength, TraintWidth));
        else
            image.Mutate(x => x.Ripple(X, Y, Radius, WaveLength, TraintWidth));
    }
}
