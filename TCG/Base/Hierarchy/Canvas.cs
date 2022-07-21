using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Metadata;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Base.Hierarchy;

public class Canvas
{
    protected Size Size { get; }
    protected List<Layer> Layers { get; }

    public Canvas(int width, int height)
    {
        Size = new Size(width, height);
        Layers = new List<Layer>();
    }

    public Layer CreateLayer(GraphicsOptions? options = null)
    {
        if (options == null) {
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

        foreach (Layer layer in Layers) {
            var renderedLayer = layer.Render();
            img.Mutate(x => x.DrawImage(renderedLayer, 1));
            renderedLayer.Dispose();
        }
        
        return img;
    }
}
