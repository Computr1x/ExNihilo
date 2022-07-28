using SixLabors.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Abstract;

namespace TCG.Rnd.Randomizers.Parameters;

public class FontParameter : GenericStructParameter<FontFamily>
{
    private FontCollection collection;

    public IEnumerable<FontFamily> FontFamilies { get => collection.Families; }

    public FontParameter(FontFamily defaultValue = default) : base(defaultValue)
    {
        collection = new FontCollection();
        collection.AddSystemFonts();
    }

    public FontParameter(FontCollection collection, FontFamily defaultValue = default) : base(defaultValue)
    {
        this.collection = collection;
    }

    protected override void RandomizeImplementation(Random r)
    {
        Value = FontFamilies.ElementAt(r.Next(FontFamilies.Count()));
    }
}
