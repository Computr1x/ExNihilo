using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of color blindness toning of the <see cref="Visual"/>
/// </summary>
public class ColorBlindness : Effect
{
    /// <summary>
    /// The type of color blindness simulator to apply.
    /// </summary>
    public EnumProperty<ColorBlindnessMode> Mode { get; set; } 
        = new EnumProperty<ColorBlindnessMode>(SixLabors.ImageSharp.Processing.ColorBlindnessMode.Achromatomaly) ;

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

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.ColorBlindness(Mode));
}