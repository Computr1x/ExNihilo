using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Crop : IEffect
{
    public RectangleParameter Rectangle { get; set; } = new();

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Crop(Rectangle));
}