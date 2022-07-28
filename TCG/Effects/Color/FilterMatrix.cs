using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects;

public class FilterMatrix : IEffect
{
    public ColorMatrix Matrix { get; set; } = new ColorMatrix();

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Filter(Matrix));
}