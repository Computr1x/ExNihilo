using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Processors;

namespace TCG.Effects
{
    public class PolarCoordinates : IEffect
    {
        public PolarConversionType PolarConversaionType { get; set; } = PolarConversionType.CartesianToPolar;

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.PolarCoordinates(PolarConversaionType));
    }
}
