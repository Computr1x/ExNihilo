using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Processors;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class PolarCoordinates : IEffect
{
    public EnumParameter<PolarConversionType> ConversionType { get; set; } = new() { Value = PolarConversionType.CartesianToPolar };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.PolarCoordinates(ConversionType.Value));
}
