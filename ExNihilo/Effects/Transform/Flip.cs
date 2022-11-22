using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of flip operations on an <see cref="Visual"/>
/// </summary>
public class Flip : Effect
{
    public override EffectType EffectType => EffectType.Transform;
    /// <summary>
    /// The <see cref="FlipMode"/> to perform the flip.
    /// </summary>
    public EnumProperty<FlipMode> Mode { get; set; } = new(FlipMode.Horizontal);

    /// <summary>
    /// <inheritdoc cref="Flip"/>
    /// </summary>
    public Flip() { }

    /// <summary>
    /// <inheritdoc cref="Flip"/>
    /// </summary>
    /// <param name="flipMode"><inheritdoc cref="Mode" path="/summary"/></param>
    public Flip(FlipMode flipMode)
    {
        Mode.Value = flipMode;
    }

    /// <summary>
    /// Set flip Mode value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Mode" path="/summary"/></param>
    public Flip WithMode(FlipMode value)
    {
        Mode.Value = value;
        return this;
    }
    /// <summary>
    /// Set Mode randomization parameters.
    /// </summary>
    /// <param name="values">Collection of flip modes for randomization.</param>
    public Flip WithRandomizedMode(IEnumerable<FlipMode> values)
    {
        Mode.EnumValues = values.ToArray();
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Flip(Mode));
}
