using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Base;

/// <summary>
/// Store visuals and effects.
/// </summary>
public class Container : Visual
{
    /// <summary>
    /// <inheritdoc cref="Container"/>
    /// </summary>
    /// <param name="size"><inheritdoc cref="Size" path="/summary"/></param>
    public Container(Size size)
    {
        Size = size;
    }

    /// <summary>
    /// <inheritdoc cref="Container"/>
    /// </summary>
    /// <param name="width">Define width of container</param>
    /// <param name="height">Define height of container</param>
    public Container(int width, int height) : this(new Size(width, height))
    {
    }

    /// <summary>
    /// Define size of container.
    /// </summary>
    public Size Size { get; }

    /// <summary>
    /// Difine collection of container visuals.
    /// </summary>
    public List<Visual> Children { get; } = new();

    /// <summary>
    /// Define container background color.
    /// </summary>
    public Color BackgroundColor { get; set; } = Color.Transparent;

    /// <summary>
    /// Define container alpha composition mode. See <see cref="PixelAlphaCompositionMode"/>.
    /// </summary>
    public PixelAlphaCompositionMode AlphaCompositionMode { get; set; } = PixelAlphaCompositionMode.SrcOver;

    /// <summary>
    /// Define container percentage of blending (0.0-1.0)
    /// </summary>
    private float blendPercentage = 1f;
    public float BlendPercentage
    {
        get => blendPercentage;
        set => blendPercentage = Math.Clamp(value, 0f, 1f);
    }

    /// <summary>
    /// Define container collor blending mode. See <see cref="PixelColorBlendingMode"/>.
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

    /// <summary>
    /// Add visual to container.
    /// </summary>
    public Container WithChild(Visual visual)
    {
        Children.Add(visual);
        return this;
    }
    
    /// <summary>
    /// Add visuals to container.
    /// </summary>
    public Container WithChildren(IEnumerable<Visual> visuals)
    {
        Children.AddRange(visuals);
        return this;
    }
    
    /// <summary>
    /// Set container background.
    /// </summary>
    public Container WithBackground(Color color)
    {
        BackgroundColor = color;
        return this;
    }
    
    /// <summary>
    /// Set container alpha composition mode.
    /// </summary>
    public Container WithAlphaCompositionMode(PixelAlphaCompositionMode mode)
    {
        AlphaCompositionMode = mode;
        return this;
    }
    
    /// <summary>
    /// Set container blend percentage.
    /// </summary>
    public Container WithBlendPercentage(float percentage)
    {
        BlendPercentage = percentage;
        return this;
    }
    
    /// <summary>
    /// Set container color blending mode.
    /// </summary>
    public Container WithColorBlendingMode(PixelColorBlendingMode mode)
    {
        ColorBlendingMode = mode;
        return this;
    }
    
    /// <summary>
    /// Set container draw antialiasing mode.
    /// </summary>
    public Container WithAntialias(bool value)
    {
        Antialias = value;
        return this;
    }
    
    /// <summary>
    /// Set container antialias subpixel depth.
    /// </summary>
    public Container WithAntialiasSubpixelDepth(int depth)
    {
        AntialiasSubpixelDepth = depth;
        return this;
    }

    /// <summary>
    /// Add container to container.
    /// </summary>
    public Container WithContainer(Container container)
    {
        Children.Add(container);
        return this;
    }

    /// <summary>
    /// Add containers to container.
    /// </summary>
    public Container WithContainers(IEnumerable<Container> containers)
    {
        Children.AddRange(containers);
        return this;
    }

    /// <summary>
    /// Add effect to visual.
    /// </summary>
    public new Container WithEffect(Effect effect)
    {
        Effects.Add(effect);
        return this;
    }
    /// <summary>
    /// Add effects to visual.
    /// </summary>
    public new Container WithEffects(IEnumerable<Effect> effects)
    {
        Effects.AddRange(effects);
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        Image<Rgba32>? tempImg = null;

        try
        {
            Visual child;

            // Background
            image.Mutate(x => x.Fill(BackgroundColor));

            // Children
            for (int i = 0; i < Children.Count; i++)
            {
                child = Children[i];

                if (child.Effects.Count == 0)
                {
                    child.Render(image, graphicsOptions);
                }
                else
                {
                    if (tempImg == null)
                        tempImg = new(Size.Width, Size.Height);
                    else
                        tempImg.Mutate(x => x.Fill(Color.Transparent));
                    // TODO: erase img instead of creating new

                    child.Render(tempImg, graphicsOptions);
                    image.Mutate(x => x.DrawImage(tempImg, graphicsOptions));
                }
            }
        }
        finally
        {
            tempImg?.Dispose();
        }

        base.Render(image, graphicsOptions);
    }

    public Image Render()
    {
        Image<Rgba32> image = new(Size.Width, Size.Height);
        
        Render(
            image,
            new()
            {
                AlphaCompositionMode = AlphaCompositionMode,
                BlendPercentage = BlendPercentage,
                ColorBlendingMode = ColorBlendingMode,
                Antialias = Antialias,
                AntialiasSubpixelDepth = AntialiasSubpixelDepth
            }
        );

        return image;
    }

    public override void Randomize(Random random, bool force = false)
    {
        base.Randomize(random, force);

        for (int i = 0; i < Children.Count; i++)
            Children[i].Randomize(random, force);
    }
}
