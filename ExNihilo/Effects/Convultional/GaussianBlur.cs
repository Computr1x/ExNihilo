using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of gaussian blur on an <see cref="Visual"/>
/// </summary>
public class GaussianBlur : Effect
{
    /// <summary>
    /// The 'sigma' (0.0-1.0) value representing the weight of the blur.
    /// </summary>
    public FloatProperty Sigma { get; set; } = new(0, 1, 0.5f) { Min = 0, Max = 1};

    /// <summary>
    /// <inheritdoc cref="GaussianBlur"/>
    /// </summary>
    public GaussianBlur() { }

    /// <summary>
    /// <inheritdoc cref="GaussianBlur"/>
    /// </summary>
    /// <param name="sigma"><inheritdoc cref="Sigma" path="/summary"/></param>
    public GaussianBlur(float sigma)
    {
        Sigma.Value = sigma;
    }

    /// <summary>
    /// Set Sigma value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Sigma" path="/summary"/></param>
    public GaussianBlur WithSigma(float value)
    {
        Sigma.Value = value;
        return this;
    }
    /// <summary>
    /// Set Sigma randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Sigma" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Sigma" path="/summary"/></param>
    public GaussianBlur WithRandomizedSigma(float min, float max)
    {
        Sigma.Min = min;
        Sigma.Max = max;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.GaussianBlur(Sigma));
}
