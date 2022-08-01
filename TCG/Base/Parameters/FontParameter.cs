using SixLabors.Fonts;
using TCG.Base.Abstract;

namespace TCG.Base.Parameters;

public class FontParameter : GenericParameter<Font>
{
    public List<Font> Fonts { get; } = new List<Font>();

    public FontParameter(Font defaultValue) : base(defaultValue) { }

    public FontParameter(Font defaultValue, Font[] fontCollection) : this(defaultValue)
    {
        Fonts.AddRange(fontCollection);
    }

    protected override void RandomizeImplementation(Random r)
    {
        Value = Fonts.Count > 0 ? Fonts[r.Next(0, Fonts.Count)] : DefaultValue;
    }
}