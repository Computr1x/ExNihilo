using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of color blindness toning of the <see cref="IDrawable"/>
/// </summary>
public class ColorBlindness : IEffect
{
    /// <summary>
    /// The type of color blindness simulator to apply.
    /// </summary>
    public EnumParameter<ColorBlindnessMode> Mode { get; set; } 
        = new EnumParameter<ColorBlindnessMode>(SixLabors.ImageSharp.Processing.ColorBlindnessMode.Achromatomaly) ;

    /// <summary>
    /// <inheritdoc cref="ColorBlindness"/>
    /// </summary>
    public ColorBlindness() { }

    /// <summary>
    /// <inheritdoc cref="ColorBlindness"/>
    /// </summary>
    /// <param name="mode"><inheritdoc cref="Mode" path="/summary"/></param>
    public ColorBlindness(ColorBlindnessMode mode)
    {
        Mode.Value = mode;
    }

    /// <summary>
    /// Set ColorBlindnessMode value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Mode" path="/summary"/></param>
    public ColorBlindness WithMode(ColorBlindnessMode value)
    {
        Mode.Value = value;
        return this;
    }

    /// <summary>
    /// Set ColorBlindnessMode randomization parameters.
    /// </summary>
    /// <param name="values">List of value for randomization</param>
    public ColorBlindness WithRandomizedMode(IEnumerable<ColorBlindnessMode> values)
    {
        Mode.EnumValues = values.ToArray();
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.ColorBlindness(Mode));
}