using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class ColorBlindness : IEffect
{
    public EnumParameter<ColorBlindnessMode> ColorBlindnessMode { get; set; } = new EnumParameter<ColorBlindnessMode>(SixLabors.ImageSharp.Processing.ColorBlindnessMode.Achromatomaly) ;

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.ColorBlindness(ColorBlindnessMode));
}