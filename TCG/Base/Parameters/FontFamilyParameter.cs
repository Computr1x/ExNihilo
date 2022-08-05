using SixLabors.Fonts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Abstract;

namespace TCG.Base.Parameters;

public class FontFamilyParameter : GenericStructParameter<FontFamily>
{
    public List<FontFamily> Collection { get; } = new List<FontFamily>();
    public CultureInfo CultureInfo { get; set; } = CultureInfo.InvariantCulture;

    public FontFamilyParameter() : base(default)
    {
    }

    public FontFamilyParameter(IEnumerable<FontFamily> collection) : base(default)
    {
        if (collection.Count() <= 0)
            throw new ArgumentException("Font collection should have at least one item");
        Collection.AddRange(collection);
    }

    protected override void RandomizeImplementation(Random r)
    {
        if (Collection.Count <= 0)
        {
            var defaultFontsCollection = new FontCollection();
            defaultFontsCollection.AddSystemFonts();

            var currentCultureFonts = defaultFontsCollection.GetByCulture(CultureInfo);
            Value = currentCultureFonts.ElementAt(r.Next(currentCultureFonts.Count()));
        }
        else
        {
            Value = Collection[r.Next(Collection.Count)];
        }
    }
}
