using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Contrast : IEffect
{
    public FloatParameter Amount { get; set; } = new FloatParameter(3) { Min = 0, Max = 3};

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Contrast(Amount));
}