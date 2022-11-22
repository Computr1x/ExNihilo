namespace ExNihilo.Base;

public enum EffectType { Color, Convultional, Distort, Noise, Transform};

public abstract class Effect : Renderable
{
    public abstract EffectType EffectType { get; }
}
