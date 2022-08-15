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
        PropertyInfo property;

        for (int i = 0; i < properties.Length; i++)
        {
            property = properties[i];

            if (!property.PropertyType.GetInterfaces().Contains(typeof(IRandomizableProperty)))
                continue;

            //Console.WriteLine(renderable.GetType().ToString() + " " + property.Name);
            (property.GetValue(source) as IRandomizableProperty)?.Randomize(_Random!, force);
        }
    }
}
