using SixLabors.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Rnd.Range;

public class FontFamiliesRange
{
    private FontCollection collection;

    public IEnumerable<FontFamily> FontFamilies { get => collection.Families; }

    public FontFamiliesRange()
    {
        collection = new FontCollection();
        collection.AddSystemFonts();
    }

    public FontFamiliesRange(FontCollection collection)
    {
        this.collection = collection;
    }
}
