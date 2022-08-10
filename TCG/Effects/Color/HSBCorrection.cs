﻿using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Define effect that allow to alter hue, brightness and saturation channel of the <see cref="IDrawable"/> 
/// </summary>
public class HSBCorrection : IEffect
{
    /// <summary>
    /// Hue shift value (-255 - 255)
    /// </summary>
    public IntParameter Hue { get; set; } = new(-255, 255, 0) { Min = sbyte.MinValue, Max = sbyte.MaxValue};
    /// <summary>
    /// Saturation shift value (-255 - 255)
    /// </summary>
    public IntParameter Saturation { get; set; } = new(-255, 255, 0) { Min = sbyte.MinValue, Max = sbyte.MaxValue };
    /// <summary>
    /// Brightness shift value (-255 - 255)
    /// </summary>
    public IntParameter Brightness { get; set; } = new(-255, 255, 0) { Min = sbyte.MinValue, Max = sbyte.MaxValue };

    /// <summary>
    /// <inheritdoc cref="HSBCorrection"/>
    /// </summary>
    public HSBCorrection() { }

    /// <summary>
    /// <inheritdoc cref="HSBCorrection"/>
    /// </summary>
    /// <param name="hue"><inheritdoc cref="Hue" path="/summary"/></param>
    /// <param name="saturation"><inheritdoc cref="Saturation" path="/summary"/></param>
    /// <param name="brightness"><inheritdoc cref="Brightness" path="/summary"/></param>
    public HSBCorrection(int hue, int saturation, int brightness)
    {
        Hue.Value = hue;
        Saturation.Value = saturation;
        Brightness.Value = brightness;
    }

    /// <summary>
    /// Set Hue value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Hue" path="/summary"/></param>
    public HSBCorrection WithHue(int value)
    {
        Hue.Value = value;
        return this;
    }
    /// <summary>
    /// Set Hue value randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Hue" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Hue" path="/summary"/></param>
    public HSBCorrection WithRandomizedHue(int min, int max)
    {
        Hue.Min = min;
        Hue.Max = max;
        return this;
    }

    /// <summary>
    /// Set Saturation value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Saturation" path="/summary"/></param>
    public HSBCorrection WithSaturation(int value)
    {
        Saturation.Value = value;
        return this;
    }
    /// <summary>
    /// Set Saturation value randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Saturation" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Saturation" path="/summary"/></param>
    public HSBCorrection WithRandomizedSaturation(int min, int max)
    {
        Saturation.Min = min;
        Saturation.Max = max;
        return this;
    }

    /// <summary>
    /// Set Brightness value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Brightness" path="/summary"/></param>
    public HSBCorrection WithBrightness(int value)
    {
        Brightness.Value = value;
        return this;
    }
    /// <summary>
    /// Set Brightness value randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Brightness" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Brightness" path="/summary"/></param>
    public HSBCorrection WithRandomizedBrightness(int min, int max)
    {
        Brightness.Min = min;
        Brightness.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.HSBCorrection(Hue, Saturation, Brightness));
}
