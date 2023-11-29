using ExNihilo.Base;
using ExNihilo.Extensions.Processors;
using ExNihilo.Processors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of wave effect on an <see cref="Visual"/>
/// </summary>
public class Wave : Effect
{
    public override EffectType EffectType => EffectType.Distort;
    /// <summary>
    /// Wave length. Must be greater or equal to 1
    /// </summary>
    public FloatProperty WaveLength { get; set; } = new(1, float.MaxValue, 3f) { Min = 1f, Max = 10f };
    /// <summary>
    /// Wave amplitude. Must be greater or equal to 1
    /// </summary>
    public FloatProperty Amplitude { get; set; } = new(1, float.MaxValue, 2f) { Min = 1f, Max = 10f };
    /// <summary>
    /// Type of wave.
    /// </summary>
    public EnumProperty<WaveType> Type { get; set; } = new(WaveType.Sine);

    /// <summary>
    /// <inheritdoc cref="Wave"/>
    /// </summary>
    public Wave() { }

    /// <summary>
    /// <inheritdoc cref="Wave"/>
    /// </summary>
    /// <param name="waveLength"><inheritdoc cref="WaveLength" path="/summary"/></param>
    /// <param name="amplitude"><inheritdoc cref="Amplitude" path="/summary"/></param>
    public Wave(float waveLength, float amplitude)
    {
        WaveLength.Value = waveLength;
        Amplitude.Value = amplitude;
    }

    /// <summary>
    /// <inheritdoc cref="Wave"/>
    /// </summary>
    /// <param name="waveLength"><inheritdoc cref="WaveLength" path="/summary"/></param>
    /// <param name="amplitude"><inheritdoc cref="Amplitude" path="/summary"/></param>
    /// /// <param name="waveType"><inheritdoc cref="Type" path="/summary"/></param>
    public Wave(float waveLength, float amplitude, WaveType waveType) : this(waveLength, amplitude)
    {
        Type.Value = waveType;
    }

    /// <summary>
    /// Set Wave length value
    /// </summary>
    /// <param name="value"><inheritdoc cref="WaveLength" path="/summary"/></param>
    public Wave WithWaveLength(float value)
    {
        WaveLength.Value = value;
        return this;
    }
    /// <summary>
    /// Set wave Length randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="WaveLength" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="WaveLength" path="/summary"/></param>
    public Wave WithRandomizedWaveLength(float min, float max)
    {
        WaveLength.WithRandomizedValue(min, max);
        return this;
    }

    /// <summary>
    /// Set Wave amplitude value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Amplitude" path="/summary"/></param>
    public Wave WithWaveAmplitude(float value)
    {
        Amplitude.Value = value;
        return this;
    }
    /// <summary>
    /// Set wave Amplitude randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Amplitude" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Amplitude" path="/summary"/></param>
    public Wave WithRandomizedAmplitude(float min, float max)
    {
        Amplitude.WithRandomizedValue(min, max);
        return this;
    }

    /// <summary>
    /// Set Wave type value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Type" path="/summary"/></param>
    public Wave WithWaveWaveType(WaveType value)
    {
        Type.Value = value;
        return this;
    }
    /// <summary>
    /// Set wave type randomization parameters.
    /// </summary>
    /// <param name="values">Collection of wave type values for randomization.</param>
    public Wave WithRandomizedWaveType(IEnumerable<WaveType> values)
    {
        Type.EnumValues = values.ToArray();
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Wave(WaveLength, Amplitude, Type));
}
