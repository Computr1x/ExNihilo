using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Base.Hierarchy;

public class Layer 
{
    private float blendPercentage = 1f;

    public readonly Size Size;
    public List<IDrawable> Drawables { get; } = new List<IDrawable>();
    public List<IEffect> Effects { get; } = new List<IEffect>();

    public PixelAlphaCompositionMode AlphaCompositionMode { get; set; } = PixelAlphaCompositionMode.SrcOver;
    public float BlendPercentage { get => blendPercentage; set => blendPercentage = Math.Clamp(value, 0f, 1f); }
    public PixelColorBlendingMode ColorBlendingMode { get; set; } = PixelColorBlendingMode.Normal;
    public bool Antialias { get; set; } = true;
    public int AntialiasSubpiexlDepth { get; set; } = 16;

    public GraphicsOptions GraphicsOptions
    {
        get
        {
            return new GraphicsOptions()
            {
                AlphaCompositionMode = AlphaCompositionMode,
                BlendPercentage = BlendPercentage,
                ColorBlendingMode = ColorBlendingMode,
                Antialias = Antialias,
                AntialiasSubpixelDepth = AntialiasSubpiexlDepth
            };
        }
    }

    public Layer(Size size)
    {
        Size = size;
    }

    public Image<Rgba32> Render()
    {
        var options = GraphicsOptions;

        Image<Rgba32> img = new(Size.Width, Size.Height);
        Image<Rgba32>? tempImg = null;

        foreach (var drawable in Drawables)
        {
            if (drawable.Effects.Count == 0)
                drawable.Render(img, options);
            else
            {
                if (tempImg == null)
                    tempImg = new(Size.Width, Size.Height);

                drawable.Render(tempImg, options);

                foreach (var effect in drawable.Effects)
                    effect.Render(tempImg, options);

                img.Mutate(x => x.DrawImage(tempImg, options));
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
