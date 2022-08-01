using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Wave : IEffect
{
    public FloatParameter WaveLength { get; set; } = new(3f) { Min = 1f, Max = 10f };
    public FloatParameter Amplitude { get; set; } = new(2f) { Min = 1f, Max = 10f };
    public EnumParameter<WaveType> Type { get; set; } = new(WaveType.Sine);

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Wave(WaveLength, Amplitude, Type));
}
