using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Processors;

namespace TCG.Effects
{
    public class Wave : IEffect
    {
        public float WaveLength { get; set; } = 3f;
        public float Amplitude { get; set; } = 2f;
        public WaveType WaveType { get; set; } = WaveType.Sine;

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.Wave(WaveLength, Amplitude, WaveType));
    }
}
