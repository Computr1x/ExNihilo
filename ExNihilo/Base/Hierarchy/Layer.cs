using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;

namespace ExNihilo.Base.Hierarchy;

/// <summary>
/// Store drawables and effects.
/// </summary>
public class Layer 
{
    private float blendPercentage = 1f;

    /// <summary>
    /// Define size of layer.
    /// </summary>
    public readonly Size Size;
    /// <summary>
    /// Difine collection of layer drawables.
    /// </summary>
    public List<IDrawable> Drawables { get; } = new List<IDrawable>();
    /// <summary>
    /// Define collection of layer effects.
    /// </summary>
    public List<IEffect> Effects { get; } = new List<IEffect>();

    /// <summary>
    /// Define layer background color.
    /// </summary>
    public Color BackgroundColor { get; set; } = Color.Transparent;
    /// <summary>
    /// Define layer alpha composition mode. See <see cref="PixelAlphaCompositionMode"/>.
    /// </summary>
    public PixelAlphaCompositionMode AlphaCompositionMode { get; set; } = PixelAlphaCompositionMode.SrcOver;
    /// <summary>
    /// Define layer percentage of blending (0.0-1.0)
    /// </summary>
    public float BlendPercentage { get => blendPercentage; set => blendPercentage = Math.Clamp(value, 0f, 1f); }
    /// <summary>
    /// Define layer collor blending mode. See <see cref="PixelColorBlendingMode"/>.
    /// </summary>
    public PixelColorBlendingMode ColorBlendingMode { get; set; } = PixelColorBlendingMode.Normal;
    /// <summary>
    /// Define if draw anatialiasing mode is enabled
    /// </summary>
    public bool Antialias { get; set; } = true;
    /// <summary>
    /// Define antialiasing subpixel depth
    /// </summary>
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

    /// <summary>
    /// <inheritdoc cref="Layer"/>
    /// </summary>
    /// <param name="size"><inheritdoc cref="Size" path="/summary"/></param>
    public Layer(Size size)
    {
        Size = size;
    }

    /// <summary>
    /// <inheritdoc cref="Layer"/>
    /// </summary>
    /// <param name="">Define width of layer</param>
    /// <param name="">Define height of layer</param>
    public Layer(int width, int height) : this(new Size(width, height))
    {
    }

    /// <summary>
    /// Add drawable to layer.
    /// </summary>
    public Layer WithDrawable(IDrawable drawable)
    {
        Drawables.Add(drawable);
        return this;
    }
    /// <summary>
    /// Add drawables to layer.
    /// </summary>
    public Layer WithDrawables(IEnumerable<IDrawable> drawables)
    {
        Drawables.AddRange(drawables);
        return this;
    }
    /// <summary>
    /// Add effect to layer.
    /// </summary>
    public Layer WithEffect(IEffect effect)
    {
        Effects.Add(effect);
        return this;
    }
    /// <summary>
    /// Add effects to layer.
    /// </summary>
    public Layer WithEffects(IEnumerable<IEffect> effects)
    {
        Effects.AddRange(effects);
        return this;
    }
    /// <summary>
    /// Set layer background.
    /// </summary>
    public Layer WithBackground(Color color)
    {
        BackgroundColor = color;
        return this;
    }
    /// <summary>
    /// Set layer alpha composition mode.
    /// </summary>
    public Layer WithAlphaCompositionMode(PixelAlphaCompositionMode mode)
    {
        AlphaCompositionMode = mode;
        return this;
    }
    /// <summary>
    /// Set layer blend percentage.
    /// </summary>
    public Layer WithBlendPercentage(float percentage)
    {
        BlendPercentage = percentage;
        return this;
    }
    /// <summary>
    /// Set layer color blending mode.
    /// </summary>
    public Layer WithColorBlendingMode(PixelColorBlendingMode mode)
    {
        ColorBlendingMode = mode;
        return this;
    }
    /// <summary>
    /// Set layer draw antialiasing mode.
    /// </summary>
    public Layer WithAntialias(bool value)
    {
        Antialias = value;
        return this;
    }
    /// <summary>
    /// Set layer antialias subpixel depth.
    /// </summary>
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
