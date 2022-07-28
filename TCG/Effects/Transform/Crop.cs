using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Crop : IEffect
{
    public RectangleParameter Rectangle { get; set; } = new(new PointParameter(new(0), new(0)), new SizeParameter(new(0), new(0)));

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Crop(Rectangle.Value));
}