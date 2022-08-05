using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class PolarCoordinates : IEffect
{
    public EnumParameter<PolarConversionType> ConversionType { get; } = new(PolarConversionType.CartesianToPolar);

    public PolarCoordinates() { }

    public PolarCoordinates(PolarConversionType conversionType)
    {
        ConversionType.Value = conversionType;
    }

    public PolarCoordinates WithType(PolarConversionType value)
    {
        ConversionType.Value = value;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.PolarCoordinates(ConversionType));
}
