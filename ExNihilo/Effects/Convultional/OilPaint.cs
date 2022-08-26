using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of oil painting effect on an <see cref="Visual"/>
/// </summary>
public class OilPaint : Effect
{
    /// <summary> The number of intensity levels. Higher values result in a broader range of color intensities forming part of the result image. </summary>
    public IntProperty Levels = new(1, int.MaxValue, 10);
    /// <summary> The number of neighboring pixels used in calculating each individual pixel value. </summary>
    public IntProperty BrushSize = new(1, int.MaxValue, 15);

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.OilPaint(Levels, BrushSize));
}