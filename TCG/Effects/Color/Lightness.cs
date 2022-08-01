using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Lightness : IEffect
{
    public FloatParameter Amount { get; set; } = new(1) { Min = 0, Max = 3};

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Lightness(Amount));
}