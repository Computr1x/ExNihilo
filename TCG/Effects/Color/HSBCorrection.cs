using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;

namespace TCG.Effects
{
    public class HSBCorrection : IEffect
    {
        private sbyte hue, saturation, brightness;

        public sbyte Hue
        {
            get => hue;
            set { hue = (sbyte)(value % 256); }
        }

        public sbyte Saturation
        {
            get => saturation;
            set { saturation = (sbyte)(value % 256); }
        }

        public sbyte Brightness
        {
            get => brightness;
            set { brightness = (sbyte)(value % 256); }
        }

        public HSBCorrection()
        {
        }

        public HSBCorrection(sbyte hue, sbyte saturation, sbyte brightness)
        {
            Hue = hue;
            Saturation = saturation;
            Brightness = brightness;
        }

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.HSBCorrection(Hue, Saturation, Brightness));
    }
}
