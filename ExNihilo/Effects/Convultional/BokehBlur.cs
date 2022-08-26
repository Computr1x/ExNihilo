using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of bokeh blur on an <see cref="Visual"/>
/// </summary>
public class BokehBlur : Effect
{
    /// <summary>  The 'radius' value representing the size of the area to sample. </summary>
    public IntProperty Radius = new(1, int.MaxValue, 32);
    /// <summary>  The 'components' value representing the number of kernels to use to approximate the bokeh effect. </summary>
    public IntProperty Components = new(1, int.MaxValue, 2);
    /// <summary>  The gamma highlight factor to use to emphasize bright spots in the source image. </summary>
    public FloatProperty Gamma = new(1, float.MaxValue, 3f);

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.BokehBlur(Radius, Components, Gamma));
}