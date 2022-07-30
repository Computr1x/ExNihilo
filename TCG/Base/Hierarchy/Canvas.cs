using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace TCG.Base.Hierarchy;

public class Canvas
{
    public Size Size { get; }
    public List<Layer> Layers { get; } = new List<Layer>();



    public Canvas(Size size)
    {
        Size = size;
    }

    public Canvas(int width, int height) : this(new Size(width, height))
    {
    }

    public Layer CreateLayer(GraphicsOptions? options = null)
    {
        if (options == null)
        {
            options = new GraphicsOptions()
            {
                AlphaCompositionMode = PixelAlphaCompositionMode.SrcOver,
                BlendPercentage = 1,
                ColorBlendingMode = PixelColorBlendingMode.Normal
            };
        }

        Layer layer = new(Size, options);
        Layers.Add(layer);
        return layer;
    }

    public Image<Rgba32> Render()
    {
        Image<Rgba32> img = new(Size.Width, Size.Height);

        foreach (Layer layer in Layers)
        {
            var renderedImage = layer.Render();

            foreach (var effect in layer.Effects)
                effect.Render(renderedImage, layer.GraphicsOptions);

            img.Mutate(x => x.DrawImage(renderedImage, 1));
            renderedImage.Dispose();
        }

        return img;
    }
}
