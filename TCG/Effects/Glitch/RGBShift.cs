using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class RGBShift : IEffect
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

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x =>
            x.RGBShift(RedXOffset, GreenXOffset, BlueXOffset, RedYOffset, GreenYOffset, BlueYOffset));

}
