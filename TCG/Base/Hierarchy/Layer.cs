using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Interfaces;

namespace TCG.Base.Hierarchy;

public class Layer
{
    protected Size Size { get; }
    protected GraphicsOptions GraphicsOptions { get; }

    public List<IDrawable> Drawables { get; }
    public List<IEffect> Effects { get; }

    public Layer(Size size, GraphicsOptions graphicsOptions)
    {
        Size = size;
        GraphicsOptions = graphicsOptions;
        Drawables = new List<IDrawable>(); 
        Effects = new List<IEffect>();
    }

    public Image<Rgba32> Render()
    {
        Image<Rgba32> img = new(Size.Width, Size.Height);
        Image<Rgba32>? tempImg = null;

        foreach (var drawable in Drawables)
        {
            if (drawable.Effects.Count == 0)
                drawable.Render(img, GraphicsOptions);
            else
            {
                if(tempImg == null)
                    tempImg = new(Size.Width, Size.Height);

                drawable.Render(tempImg, GraphicsOptions);

                foreach (var effect in drawable.Effects)
                    effect.Render(tempImg, GraphicsOptions);

                img.Mutate(x => x.DrawImage(tempImg, GraphicsOptions));
            }
        }
        tempImg?.Dispose();

        return img;
    }
}
