using ExNihilo.Rnd;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ExNihilo.Base.Interfaces;

public abstract class Drawable : Renderable
{
    /// <summary>
    /// Defines collection of effects. Effect will be applied after all drawables will be rendered.
    /// </summary>
    public List<Effect> Effects { get; private set; } = new();

    /// <summary>
    /// Add effect to drawable.
    /// </summary>
    public Drawable WithEffect(Effect effect)
    {
        Effects.Add(effect);
        return this;
    }
    /// <summary>
    /// Add effects to drawable.
    /// </summary>
    public Drawable WithEffects(IEnumerable<Effect> effects)
    {
        Effects.AddRange(effects);
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        foreach (var effect in Effects)
            effect.Render(image, graphicsOptions);
    }

    public virtual void Randomize(bool force = false)
    {
        RandomManager.RandomizeProperties(this, force);

        for (int i = 0; i < Effects.Count; i++)
            RandomManager.RandomizeProperties(Effects[i], force);
    }
}
