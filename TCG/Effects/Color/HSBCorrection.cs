using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class HSBCorrection : IEffect
    {
        private byte hue, saturation, brightness;

        public byte Hue
        {
            get => hue;
            set { hue = (byte)(value % 256); }
        }

        public byte Saturation
        {
            get => saturation;
            set { saturation = (byte)(value % 256); }
        }

        public byte Brightness
        {
            get => brightness;
            set { brightness = (byte)(value % 256); }
        }

        public HSBCorrection()
        {
        }

        public HSBCorrection(byte hue, byte saturation, byte brightness)
        {
            Hue = hue;
            Saturation = saturation;
            Brightness = brightness;
        }

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.HSBCorrection());
    }
}
