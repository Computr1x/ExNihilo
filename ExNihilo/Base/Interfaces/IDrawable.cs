namespace ExNihilo.Base.Interfaces;

public interface IDrawable : IRenderable
{
    public IList<IEffect> Effects { get; }
}
