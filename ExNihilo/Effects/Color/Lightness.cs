﻿using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow to alter brightness component of the <see cref="Visual"/>
/// </summary>
public class Lightness : Effect
{
    public override EffectType EffectType => EffectType.Color;
    /// <summary>
    /// The proportion of the conversion. Must be greater than or equal to 0.
    /// </summary>
    public FloatProperty Amount { get; set; } = new(1) { Min = 0, Max = 3};

    /// <summary>
    /// <inheritdoc cref="Lightness"/>
    /// </summary>
    public Lightness() { }

    /// <summary>
    /// <inheritdoc cref="Lightness"/>
    /// </summary>
    /// <param name="amount"><inheritdoc cref="Amount" path="/summary"/></param>
    public Lightness(float amount)
    {
        Amount.Value = amount;
    }

    /// <summary>
    /// Set Amount value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Amount" path="/summary"/></param>
    public Lightness WithAmount(float value)
    {
        Amount.Value = value;
        return this;
    }
    /// <summary>
    /// Set Amount value randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Amount" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Amount" path="/summary"/></param>
    public Lightness WithRandomizedAmount(float min, float max)
    {
        Amount.Min = min;
        Amount.Max = max;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Lightness(Amount));
}