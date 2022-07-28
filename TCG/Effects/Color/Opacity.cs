using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Opacity : IEffect
{
    // beteween 0 - 1
    public FloatParameter Amount { get; set; } = new(0, 1) { Value = 0.5f };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Opacity(Amount.Value));
}
