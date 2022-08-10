using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of flip operations on an <see cref="IDrawable"/>
/// </summary>
public class Flip : IEffect
{
    /// <summary>
    /// The <see cref="FlipMode"/> to perform the flip.
    /// </summary>
    public EnumParameter<FlipMode> Mode { get; set; } = new(FlipMode.Horizontal);

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

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Flip(Mode));
}
