using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class Bulge : IEffect
    {
        public int X { get; }
        public int Y { get; }
        public float Radius { get; }
        public float Strenght { get; }

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.Bulge());
    }
}
