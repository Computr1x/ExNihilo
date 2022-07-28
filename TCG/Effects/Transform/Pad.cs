using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Pad : IEffect
{
    public IntParameter Width { get; set; } = new(0) { Min = 0, Max = 50 };
    public IntParameter Height { get; set; } = new(0) { Min = 0, Max = 50 };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Pad(Width, Height));
}
