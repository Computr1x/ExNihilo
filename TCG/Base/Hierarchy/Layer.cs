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

    public Color BackgroundColor { get; set; } = Color.Transparent;
    public PixelAlphaCompositionMode AlphaCompositionMode { get; set; } = PixelAlphaCompositionMode.SrcOver;
    public float BlendPercentage { get => blendPercentage; set => blendPercentage = Math.Clamp(value, 0f, 1f); }
    public PixelColorBlendingMode ColorBlendingMode { get; set; } = PixelColorBlendingMode.Normal;
    public bool Antialias { get; set; } = true;
    public int AntialiasSubpixelDepth { get; set; } = 16;

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
                AntialiasSubpixelDepth = AntialiasSubpixelDepth
            };
        }
    }

    public Layer(Size size)
    {
        Size = size;
    }

    public Layer(int width, int height) : this(new Size(width, height))
    {
    }

    public Layer WithDrawable(IDrawable drawable)
    {
        Drawables.Add(drawable);
        return this;
    }

    public Layer WithDrawables(IEnumerable<IDrawable> drawables)
    {
        Drawables.AddRange(drawables);
        return this;
    }

    public Layer WithEffect(IEffect effect)
    {
        Effects.Add(effect);
        return this;
    }

    public Layer WithEffects(IEnumerable<IEffect> effects)
    {
        Effects.AddRange(effects);
        return this;
    }

    public Layer WithBackground(Color color)
    {
        BackgroundColor = color;
        return this;
    }

    public Layer WithAlphaCompositionMode(PixelAlphaCompositionMode mode)
    {
        AlphaCompositionMode = mode;
        return this;
    }

    public Layer WithBlendPercentage(float percentage)
    {
        BlendPercentage = percentage;
        return this;
    }

    public Layer WithColorBlendingMode(PixelColorBlendingMode mode)
    {
        ColorBlendingMode = mode;
        return this;
    }

    public Layer WithAntialias(bool value)
    {
        Antialias = value;
        return this;
    }

    public Layer WithAntialiasSubpixelDepth(int depth)
    {
        AntialiasSubpixelDepth = depth;
        return this;
    }

    public Image<Rgba32> Render()
    {
        var options = GraphicsOptions;

        Image<Rgba32> img = new(Size.Width, Size.Height, BackgroundColor.ToPixel<Rgba32>());
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
}
