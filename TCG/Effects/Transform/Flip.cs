using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Flip : IEffect
{
    public EnumParameter<FlipMode> Mode { get; set; } = new() { Value = FlipMode.Horizontal };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Flip(Mode.Value));
}
