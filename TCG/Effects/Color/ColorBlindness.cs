using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class ColorBlindness : IEffect
{
    public EnumParameter<ColorBlindnessMode> ColorBlindnessModeParameter { get; set; } = new EnumParameter<ColorBlindnessMode>() { Value = ColorBlindnessMode.Achromatomaly };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.ColorBlindness(ColorBlindnessModeParameter.Value));
}