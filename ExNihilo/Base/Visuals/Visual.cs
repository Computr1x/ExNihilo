using ExNihilo.Rnd;
using SixLabors.ImageSharp;

namespace ExNihilo.Base;

[Flags]
public enum VisualType : byte
{
    Filled = 1 << 0,
    Outlined = 1 << 1,
    FillWithOutline = Filled | Outlined
}

public abstract class Visual : Renderable
{
    /// <summary>
    /// Defines collection of effects. Effect will be applied after all visuals will be rendered.
    /// </summary>
    public List<Effect> Effects { get; private set; } = new();

    /// <summary>
    /// Add effect to visual.
    /// </summary>
    public Visual WithEffect(Effect effect)
    {
        Effects.Add(effect);
        return this;
    }
    /// <summary>
    /// Add effects to visual.
    /// </summary>
    public Visual WithEffects(IEnumerable<Effect> effects)
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
