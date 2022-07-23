using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;

namespace TCG.Effects
{
    public class Ripple : IEffect
    {
        // center of Ripple, by default center of image
        public int X { get; set; }
        public int Y { get; set; }
        // radius of effect in pixels
        public float Radius { get; } = 100f;
        //  wavelength of ripples, in pixels
        public float WaveLength { get; } = 10f;
        // approximate width of wave train, in wavelengths
        public float TraintWidth { get; set; } = 2f;

        public void Render(Image image, GraphicsOptions graphicsOptions)
        {
            if(X == 0&& Y == 0)
                image.Mutate(x => x.Ripple(Radius, WaveLength, TraintWidth));
            else
                image.Mutate(x => x.Ripple(X, Y, Radius, WaveLength, TraintWidth));
        }
    }
}
