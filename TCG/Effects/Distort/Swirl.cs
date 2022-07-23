using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;

namespace TCG.Effects
{
    public class Swirl : IEffect
    {
        public float Degree { get; set; }
        public float Radius { get; set; }
        public float Twists { get; set; }

        public int X { get; }
        public int Y { get; }

        public void Render(Image image, GraphicsOptions graphicsOptions)
        {
            if (X == 0 && Y == 0)
                image.Mutate(x => x.Swirl(Radius, Degree, Twists));
            else
                image.Mutate(x => x.Swirl(X, Y, Radius, Degree, Twists));
        }
    }
}
