using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;
using ExNihilo.Extensions.Processors;
using ExNihilo.Base.Properties;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of slitscan effect on an <see cref="Drawable"/>
/// </summary>
public class SlitScan : Effect
{
    /// <summary>
    /// SlitScan time value. Must be greater then 0.
    /// </summary>
    public FloatProperty Time { get; set; } = new(0, float.MaxValue, 2f) { Min = 1f, Max = 10f };

    /// <summary>
    /// <inheritdoc cref="SlitScan"/>
    /// </summary>
    public SlitScan() { }

    /// <summary>
    /// <inheritdoc cref="SlitScan"/>
    /// </summary>
    /// <param name="time"><inheritdoc cref="Time" path="/summary"/></param>
    public SlitScan(float time)
    {
        Time.Value = time;
    }

    /// <summary>
    /// Set Time value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Time" path="/summary"/></param>
    public SlitScan WithTime(float value)
    {
        Time.Value = value;
        return this;
    }
    /// <summary>
    /// Set slitscan Time randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Time" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Time" path="/summary"/></param>
    public SlitScan WithRandomizedTime(float min, float max)
    {
        Time.Min = min;
        Time.Max = max;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.SlitScan(Time));
}