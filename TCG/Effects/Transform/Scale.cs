using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class Scale : IEffect
    {
        public float XScale { get; set; }
        public float YScale { get; set; }

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => 
                x.Transform(new AffineTransformBuilder().AppendScale(new System.Numerics.Vector2(XScale, YScale))));
    }
}
