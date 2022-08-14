using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;

namespace ExNihilo.Base.Hierarchy;

/// <summary>
/// Stores image layers and effects.
/// </summary>
public class Canvas
{
    /// <summary>
    /// Define size of canvas.
    /// </summary>
    public Size Size { get; }
    /// <summary>
    /// Define collection of canvas layers.
    /// </summary>
    public List<Layer> Layers { get; } = new List<Layer>();
    /// <summary>
    /// Define collection of canvas effects. Effect will be applied after all drawables will be rendered.
    /// </summary>
    public List<IEffect> Effects { get; } = new List<IEffect>();

    /// <summary>
    /// <inheritdoc cref="Canvas"/>
    /// </summary>
    /// <param name="size"><inheritdoc cref="Size" path="/summary"/></param>
    public Canvas(Size size)
    {
        Size = size;
    }

    /// <summary>
    /// <inheritdoc cref="Canvas"/>
    /// </summary>
    /// <param name="width">Define width of canvas.</param>
    /// <param name="height">Define height of canvas.</param>
    public Canvas(int width, int height) : this(new Size(width, height))
    {
    }

    public Image<Rgba32> Render()
    {
        Image<Rgba32> img = new(Size.Width, Size.Height);

        foreach (Layer layer in Layers)
        {
            var renderedImage = layer.Render();

            foreach (var effect in layer.Effects)
                effect.Render(renderedImage, layer.GraphicsOptions);

            img.Mutate(x => x.DrawImage(renderedImage, layer.GraphicsOptions));
            renderedImage.Dispose();
        }

        foreach (var effect in Effects)
            effect.Render(img, new GraphicsOptions());

        return img;
    }

    /// <summary>
    /// Add layer to canvas.
    /// </summary>
    public Canvas WithLayer(Layer layer)
    {
        Layers.Add(layer);
        return this;
    }

    /// <summary>
    /// Add layers to canvas.
    /// </summary>
    public Canvas WithLayers(IEnumerable<Layer> layers)
    {
        Layers.AddRange(layers);
        return this;
    }

    /// <summary>
    /// Add effect to canvas
    /// </summary>
    public Canvas WithEffect(IEffect effect)
    {
        Effects.Add(effect);
        return this;
    }

    /// <summary>
    /// Add effects to canvas
    /// </summary>
    public Canvas WithEffects(IEnumerable<IEffect> effects)
    {
        Effects.AddRange(effects);
        return this;
    }
}
