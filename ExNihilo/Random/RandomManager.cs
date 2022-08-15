using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using System.Numerics;
using System.Text;
using ExNihilo.Base.Hierarchy;
using ExNihilo.Base.Interfaces;
using ExNihilo.Base.Parameters;
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

    public static void RandomizeLayer(Layer layer, bool force = false)
    {
        IDrawable drawable;

        for (int i = 0; i < layer.Drawables.Count; i++)
        {
            drawable = layer.Drawables[i];

            RandomizeProperties(drawable, force);

            for (int j = 0; j < drawable.Effects.Count; j++)
                RandomizeProperties(drawable.Effects[j], force);
        }

        for (int i = 0; i < layer.Effects.Count; i++)
            RandomizeProperties(layer.Effects[i], force);
    }

    public void RandomizeCanvas(Canvas canvas, bool force = false)
    {
        for (int i = 0; i < canvas.Layers.Count; i++)
            RandomizeLayer(canvas.Layers[i], force);

        for (int i = 0; i < canvas.Effects.Count; i++)
            RandomizeProperties(canvas.Effects[i], force);
    }

    public static void RandomizeProperties(object source, bool force = false)
    {
        PropertyInfo[] properties = source.GetType().GetProperties();
        PropertyInfo property;

        for (int i = 0; i < properties.Length; i++)
        {
            property = properties[i];

            if (!property.PropertyType.GetInterfaces().Contains(typeof(IRandomizableParameter)))
                continue;

            //Console.WriteLine(renderable.GetType().ToString() + " " + property.Name);
            (property.GetValue(source) as IRandomizableParameter)?.Randomize(_Random!, force);
        }
    }
}
