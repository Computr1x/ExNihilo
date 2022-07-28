using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Processors;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Wave : IEffect
{
    public FloatParameter WaveLength { get; set; } = new(25f) { Value = 3f };
    public FloatParameter Amplitude { get; set; } = new(10f) { Value = 2f };
    public EnumParameter<WaveType> Type { get; set; } = new() { Value = WaveType.Sine};

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Wave(WaveLength.Value, Amplitude.Value, Type.Value));
}
