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

    public Wave() { }

    public Wave(float waveLength, float amplitude)
    {
        WaveLength.Value = waveLength;
        Amplitude.Value = amplitude;
    }

    public Wave(float waveLength, float amplitude, WaveType waveType) : this(waveLength, amplitude)
    {
        Type.Value = waveType;
    }

    public Wave WithWaveLength(float value)
    {
        WaveLength.Value = value;
        return this;
    }

    public Wave WithRandomizedWaveLength(float min, float max)
    {
        WaveLength.Min = min;
        WaveLength.Max = max;
        return this;
    }

    public Wave WithWaveAmplitude(float value)
    {
        Amplitude.Value = value;
        return this;
    }

    public Wave WithRandomizedAmplitude(float min, float max)
    {
        Amplitude.Min = min;
        Amplitude.Max = max;
        return this;
    }

    public Wave WithWaveWaveType(WaveType value)
    {
        Type.Value = value;
        return this;
    }

    public Wave WithRandomizedWaveType(IEnumerable<WaveType> values)
    {
        Type.EnumValues = values.ToArray();
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Wave(WaveLength, Amplitude, Type));
}
