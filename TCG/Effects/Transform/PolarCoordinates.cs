using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the translate Euclidean coordinates to Polar coordinats and vise versa on an <see cref="IDrawable"/>
/// </summary>
public class PolarCoordinates : IEffect
{
    /// <summary>
    /// The <see cref="PolarConversionType"/> to perform the translate.
    /// </summary>
    public EnumParameter<PolarConversionType> ConversionType { get; } = new(PolarConversionType.CartesianToPolar);

    /// <summary>
    /// <inheritdoc cref="PolarCoordinates"/>
    /// </summary>
    public PolarCoordinates() { }

    /// <summary>
    /// <inheritdoc cref="PolarCoordinates"/>
    /// </summary>
    /// <param name="conversionType"><inheritdoc cref="ConversionType" path="/summary"/></param>
    public PolarCoordinates(PolarConversionType conversionType)
    {
        ConversionType.Value = conversionType;
    }
    /// <summary>
    /// Set polar conversion Type value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Type" path="/summary"/></param>
    public PolarCoordinates WithType(PolarConversionType value)
    {
        ConversionType.Value = value;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.PolarCoordinates(ConversionType));
}
