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

        return img;
    }
}
