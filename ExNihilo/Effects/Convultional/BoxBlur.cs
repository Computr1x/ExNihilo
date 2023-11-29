using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of box blur on an <see cref="Visual"/>
/// </summary>
public class BoxBlur : Effect
{
    public override EffectType EffectType => EffectType.Convultional;
    /// <summary>  The 'radius' value representing the size of the area to sample. </summary>
    public IntProperty Radius = new(1, int.MaxValue, 32);

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.BoxBlur(Radius));
}