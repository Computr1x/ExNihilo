﻿using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class ColorBlindness : IEffect
{
    public EnumParameter<ColorBlindnessMode> ColorBlindnessMode { get; set; } 
        = new EnumParameter<ColorBlindnessMode>(SixLabors.ImageSharp.Processing.ColorBlindnessMode.Achromatomaly) ;

    public ColorBlindness() { }

    public ColorBlindness(ColorBlindnessMode mode)
    {
        ColorBlindnessMode.Value = mode;
    }

    public ColorBlindness WithMode(ColorBlindnessMode value)
    {
        ColorBlindnessMode.Value = value;
        return this;
    }

    public ColorBlindness WithRandomizedMode(IEnumerable<ColorBlindnessMode> values)
    {
        ColorBlindnessMode.EnumValues = values.ToArray();
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.ColorBlindness(ColorBlindnessMode));
}