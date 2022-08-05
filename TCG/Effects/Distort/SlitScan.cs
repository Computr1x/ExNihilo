using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class SlitScan : IEffect
{
    public FloatParameter Time { get; set; } = new(2f) { Min = 1f, Max = 10f };

    public SlitScan() { }

    public SlitScan(float time)
    {
        Time.Value = time;
    }

    public SlitScan WithTime(float value)
    {
        Time.Value = value;
        return this;
    }

    public SlitScan WithRandomizedTime(float min, float max)
    {
        Time.Min = min;
        Time.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.SlitScan(Time));
}