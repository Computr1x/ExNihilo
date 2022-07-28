using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Lightness : IEffect
{
    public FloatParameter Amount { get; set; } = new(5f) { Value = 1f };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Lightness(Amount.Value));
}