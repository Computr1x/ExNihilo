using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using System.Numerics;
using System.Text;
using ExNihilo.Base;
using ExNihilo.Base.Interfaces;
using ExNihilo.Base.Properties;
using System.Reflection;

namespace ExNihilo.Rnd;

public class RandomManager
{
    private static Random? _Random;

    public int Seed { get; private set; }

    public RandomManager(int seed)
    {
        ResetRandom(seed);
    }

    public void ResetRandom()
    {
        _Random = new(Seed);
    }

    public void ResetRandom(int seed)
    {
        Seed = seed;
        ResetRandom();
    }

    public static void RandomizeProperties(object source, bool force = false)
    {
        PropertyInfo[] properties = source.GetType().GetProperties();
        PropertyInfo propertyInfo;

        for (int i = 0; i < properties.Length; i++)
        {
            propertyInfo = properties[i];

            if (propertyInfo.GetValue(source) is Property property)
                property.Randomize(_Random!, force);
        }
    }
}
