using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of resize operations on an <see cref="IDrawable"/>
/// </summary>
public class Resize : IEffect
{
    /// <summary>
    /// The target image width.
    /// </summary>
    public IntParameter Width { get; set; } = new(1, int.MaxValue, 0);
    /// <summary>
    /// The target image height.
    /// </summary>
    public IntParameter Height { get; set; } = new(1, int.MaxValue, 0);

    public Resize() { }

    public Resize(int width, int height)
    {
        Width.Value = width;
        Height.Value = height;
    }

    public Resize WithWidth(int value)
    {
        Width.Value = value;
        return this;
    }

    public Resize WithRandomizedWidth(int min, int max)
    {
        Width.Min = min;
        Width.Max = max;
        return this;
    }

    public Resize WithHeight(int value)
    {
        Height.Value = value;
        return this;
    }

    public Resize WithRandomizedHeight(int min, int max)
    {
        Height.Min = min;
        Height.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Resize(Width, Height));
}