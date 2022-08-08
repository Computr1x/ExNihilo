using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of wave effect on an <see cref="IDrawable"/>
/// </summary>
public class Wave : IEffect
{
    /// <summary>
    /// Wave length. Must be greater or equal to 0
    /// </summary>
    public FloatParameter WaveLength { get; set; } = new(0, float.MaxValue, 3f) { Min = 1f, Max = 10f };
    /// <summary>
    /// Wave amplitude. Must be greater or equal to 0
    /// </summary>
    public FloatParameter Amplitude { get; set; } = new(0, float.MaxValue, 2f) { Min = 1f, Max = 10f };
    /// <summary>
    /// Type of wave.
    /// </summary>
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
