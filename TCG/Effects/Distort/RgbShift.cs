using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of slices effect on an <see cref="IDrawable"/>
/// </summary>
public class RgbShift : IEffect
{
    /// <summary>
    /// Shift offset value by all channel. 
    /// </summary>
    public int Offset
    {
        set
        {
            RedXOffset.Value = RedYOffset.Value = value;
            GreenXOffset.Value = GreenYOffset.Value = -value;
            BlueXOffset.Value = value;
            BlueYOffset.Value = -value;
        }
    }

    /// <summary>
    /// Shift offset by y of blue channel
    /// </summary>
    public IntParameter BlueYOffset { get; set; } = new(0) { Min = -10, Max = 10};
    /// <summary>
    /// Shift offset by y of green channel
    /// </summary>
    public IntParameter GreenYOffset { get; set; } = new(0) { Min = -10, Max = 10 };
    /// <summary>
    /// Shift offset by y of red channel
    /// </summary>
    public IntParameter RedYOffset { get; set; } = new(0) { Min = -10, Max = 10 };
    /// <summary>
    /// Shift offset by x of blue channel
    /// </summary>
    public IntParameter BlueXOffset { get; set; } = new(0) { Min = -10, Max = 10 };
    /// <summary>
    /// Shift offset by x of green channel
    /// </summary>
    public IntParameter GreenXOffset { get; set; } = new(0) { Min = -10, Max = 10 };
    /// <summary>
    /// Shift offset by x of red channel
    /// </summary>
    public IntParameter RedXOffset { get; set; } = new(0) { Min = -10, Max = 10 };

    /// <summary>
    /// <inheritdoc cref="RgbShift"/>
    /// </summary>
    public RgbShift() { }

    /// <summary>
    /// <inheritdoc cref="RgbShift"/>
    /// </summary>
    /// <param name="offset"><inheritdoc cref="Offset" path="/summary"/></param>
    public RgbShift(int offset)
    {
        Offset = offset;
    }

    /// <summary>
    /// <inheritdoc cref="RgbShift"/>
    /// </summary>
    /// <param name="redXOffset"><inheritdoc cref="RedXOffset" path="/summary"/></param>
    /// <param name="redYOffset"><inheritdoc cref="RedYOffset" path="/summary"/></param>
    /// <param name="greenXOffset"><inheritdoc cref="GreenXOffset" path="/summary"/></param>
    /// <param name="greenYOffset"><inheritdoc cref="GreenYOffset" path="/summary"/></param>
    /// <param name="blueXOffset"><inheritdoc cref="BlueXOffset" path="/summary"/></param>
    /// <param name="blueYOffset"><inheritdoc cref="BlueYOffset" path="/summary"/></param>
    public RgbShift(int redXOffset, int redYOffset, int greenXOffset, int greenYOffset, int blueXOffset, int blueYOffset)
    {
        RedXOffset.Value = redXOffset;
        RedYOffset.Value = redYOffset;
        GreenXOffset.Value = greenXOffset;
        GreenYOffset.Value = greenYOffset;
        BlueXOffset.Value = blueXOffset;
        BlueYOffset.Value = blueYOffset;
    }

    /// <summary>
    /// Set Offset value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Offset" path="/summary"/></param>
    public RgbShift WithOffset(int value)
    {
        Offset = value;
        return this;
    }

    /// <summary>
    /// Set Offset randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Offset" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Offset" path="/summary"/></param>
    public RgbShift WithRandomizedOffset(int min, int max)
    {
        RedXOffset.Min = RedYOffset.Min = GreenXOffset.Min = GreenYOffset.Min = BlueXOffset.Min = BlueYOffset.Min = min;
        RedXOffset.Max = RedYOffset.Max = GreenXOffset.Max = GreenYOffset.Max = BlueXOffset.Max = BlueYOffset.Max = max;
        return this;
    }

    /// <summary>
    /// Set BlueXOffset value
    /// </summary>
    /// <param name="value"><inheritdoc cref="BlueXOffset" path="/summary"/></param>
    public RgbShift WithBlueXOfffset(int value)
    {
        BlueXOffset.Value = value;
        return this;
    }
    /// <summary>
    /// Set GreenXOffset value
    /// </summary>
    /// <param name="value"><inheritdoc cref="GreenXOffset" path="/summary"/></param>
    public RgbShift WithGreenXOffset(int value)
    {
        GreenXOffset.Value = value;
        return this;
    }
    /// <summary>
    /// Set RedXOffset value
    /// </summary>
    /// <param name="value"><inheritdoc cref="RedXOffset" path="/summary"/></param>
    public RgbShift WithRedXOfffset(int value)
    {
        RedXOffset.Value = value;
        return this;
    }
    /// <summary>
    /// Set BlueYOffset value
    /// </summary>
    /// <param name="value"><inheritdoc cref="BlueYOffset" path="/summary"/></param>
    public RgbShift WithBlueYOfffset(int value)
    {
        BlueYOffset.Value = value;
        return this;
    }
    /// <summary>
    /// Set GreenYOffset value
    /// </summary>
    /// <param name="value"><inheritdoc cref="GreenYOffset" path="/summary"/></param>
    public RgbShift WithGreenYOffset(int value)
    {
        GreenYOffset.Value = value;
        return this;
    }
    /// <summary>
    /// Set RedYOffset value
    /// </summary>
    /// <param name="value"><inheritdoc cref="RedYOffset" path="/summary"/></param>
    public RgbShift WithRedYOfffset(int value)
    {
        RedYOffset.Value = value;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x =>
            x.RgbShift(RedXOffset, GreenXOffset, BlueXOffset, RedYOffset, GreenYOffset, BlueYOffset));

}
