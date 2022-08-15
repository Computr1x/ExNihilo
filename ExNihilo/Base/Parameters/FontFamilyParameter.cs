using SixLabors.Fonts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExNihilo.Base.Abstract;

namespace ExNihilo.Base.Parameters;

public class FontFamilyParameter : GenericStructParameter<FontFamily>
{
    public List<FontFamily> Collection { get; } = new List<FontFamily>();
    public CultureInfo CultureInfo { get; set; } = CultureInfo.InvariantCulture;

    public FontFamilyParameter() : base(default)
    {
    }

    public FontFamilyParameter(IEnumerable<FontFamily> collection) : base(default)
    {
        if (!collection.Any())
            throw new ArgumentException("Font collection should have at least one item");
       
        Collection.AddRange(collection);
    }

    public FontFamilyParameter WithRandomizedValue(IEnumerable<FontFamily> values)
    {
        Collection.Clear();
        Collection.AddRange(values);

        return this;
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
