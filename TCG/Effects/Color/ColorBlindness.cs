using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class ColorBlindness : IEffect
    {
        public ColorBlindnessMode ColorBlindnessMode { get; set; } = ColorBlindnessMode.Achromatomaly;

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.ColorBlindness(ColorBlindnessMode));
    }
}