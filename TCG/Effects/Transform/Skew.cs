using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class Skew : IEffect
    {
        public float XDegrees { get; set; }
        public float YDegrees { get; set; }

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.
            x.Transform(new AffineTransformBuilder().AppendSkewDegrees(XDegrees, YDegrees)));
    }
}
