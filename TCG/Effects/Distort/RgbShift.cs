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

    public RgbShift() { }

    public RgbShift(int offset)
    {
        Offset = offset;
    }

    public RgbShift(int redXOffset, int redYOffset, int greenXOffset, int greenYOffset, int blueXOffset, int blueYOffset)
    {
        RedXOffset.Value = redXOffset;
        RedYOffset.Value = redYOffset;
        GreenXOffset.Value = greenXOffset;
        GreenYOffset.Value = greenYOffset;
        BlueXOffset.Value = blueXOffset;
        BlueYOffset.Value = blueYOffset;
    }

    public RgbShift WithOffset(int value)
    {
        Offset = value;
        return this;
    }

    public RgbShift WithBlueXOfffset(int value)
    {
        BlueXOffset.Value = value;
        return this;
    }

    public RgbShift WithGreenXOffset(int value)
    {
        GreenXOffset.Value = value;
        return this;
    }

    public RgbShift WithRedXOfffset(int value)
    {
        RedXOffset.Value = value;
        return this;
    }

    public RgbShift WithBlueYOfffset(int value)
    {
        BlueYOffset.Value = value;
        return this;
    }

    public RgbShift WithGreenYOffset(int value)
    {
        GreenYOffset.Value = value;
        return this;
    }

    public RgbShift WithRedYOfffset(int value)
    {
        RedYOffset.Value = value;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x =>
            x.RgbShift(RedXOffset, GreenXOffset, BlueXOffset, RedYOffset, GreenYOffset, BlueYOffset));

}
