using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Resize : IEffect
{
    public IntParameter Width { get; set; } = new(0);
    public IntParameter Height { get; set; } = new(0);

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Resize(Width.Value, Height.Value));
}