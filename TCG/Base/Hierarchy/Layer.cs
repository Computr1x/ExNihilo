using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Base.Hierarchy;

public class Layer 
{
    public Size Size { get; }
    public GraphicsOptions GraphicsOptions { get; }

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
                if (tempImg == null)
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

    //public void Randomize(Random r, bool force = false)
    //{
    //    // TODO MAKE LAYER RANDOMIZE
    //    foreach(var effect in Effects)


    //}
}
