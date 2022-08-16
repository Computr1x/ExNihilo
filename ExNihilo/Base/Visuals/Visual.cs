﻿using ExNihilo.Rnd;
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
    public List<Effect> Effects { get; } = new();

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="image"></param>
    /// <param name="graphicsOptions"></param>
    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        for (int i = 0; i < Effects.Count; i++)
            Effects[i].Render(image, graphicsOptions);
    }

    public virtual void Randomize(Random random, bool force = false)
    {
        RandomizeProperties(random, force);

        for (int i = 0; i < Effects.Count; i++)
            Effects[i].RandomizeProperties(random, force);
    }
}