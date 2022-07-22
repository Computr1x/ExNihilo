using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class Shift : IEffect
    {
        public float XShift { get; set; }
        public float YShift { get; set; }

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.Shift());
    }
}
