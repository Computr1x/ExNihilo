using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of edge detection effect on an <see cref="Visual"/>
/// </summary>
public class EdgeDetection : Effect
{
    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.DetectEdges());
}
