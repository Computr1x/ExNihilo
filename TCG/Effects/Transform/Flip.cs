using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Flip : IEffect
{
    public EnumParameter<FlipMode> Mode { get; set; } = new(FlipMode.Horizontal);

    public Flip() { }

    public Flip(FlipMode flipMode)
    {
        Mode.Value = flipMode;
    }

    public Flip WithMode(FlipMode value)
    {
        Mode.Value = value;
        return this;
    }

    public Flip WithRandomizedMode(IEnumerable<FlipMode> values)
    {
        Mode.EnumValues = values.ToArray();
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Flip(Mode));
}
