using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Rotate : IEffect
{
    public FloatParameter Degree { get; set; } = new(0) { Min = 0, Max = 360 };

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
