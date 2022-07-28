using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Ripple : IEffect
{
    public IntParameter X { get; set; } = new(0);
    public IntParameter Y { get; set; } = new(0);
    // radius of effect in pixels
    public FloatParameter Radius { get; set; } = new(100f) { Value = 100f };
    //  wavelength of ripples, in pixels
    public FloatParameter WaveLength { get; set; } = new FloatParameter(10f) { Value = 10f };
    // approximate width of wave train, in wavelengths
    public FloatParameter TraintWidth { get; set; } = new(10f) { Value = 2f };

    public void Render(Image image, GraphicsOptions graphicsOptions)
    {
        if (X.Value == 0 && Y.Value == 0)
            image.Mutate(x => x.Ripple(Radius.Value, WaveLength.Value, TraintWidth.Value));
        else
            image.Mutate(x => x.Ripple(X.Value, Y.Value, Radius.Value, WaveLength.Value, TraintWidth.Value));
    }
}
