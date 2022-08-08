using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of rotation operations on an <see cref="IDrawable"/>
/// </summary>
public class Rotate : IEffect
{
    /// <summary>
    /// Amount of rotation in degrees (-360-360).
    /// </summary>
    public FloatParameter Degree { get; set; } = new(-360, 360, 0) { Min = 0, Max = 360 };

    public Rotate() { }

    public Rotate(float degree)
    {
        Degree.Value = degree;
    }

    public Rotate WithDegree(float value)
    {
        Degree.Value = value;
        return this;
    }

    public Rotate WithRandomizedDegree(float min, float max)
    {
        Degree.Min = min;
        Degree.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Rotate(Degree));
}
