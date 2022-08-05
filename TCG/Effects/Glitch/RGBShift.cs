using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class RgbShift : IEffect
{
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

    public IntParameter BlueYOffset { get; set; } = new(0) { Min = -10, Max = 10};
    public IntParameter GreenYOffset { get; set; } = new(0) { Min = -10, Max = 10 };
    public IntParameter RedYOffset { get; set; } = new(0) { Min = -10, Max = 10 };
    public IntParameter BlueXOffset { get; set; } = new(0) { Min = -10, Max = 10 };
    public IntParameter GreenXOffset { get; set; } = new(0) { Min = -10, Max = 10 };
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
