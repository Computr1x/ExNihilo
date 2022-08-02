using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using System.Numerics;
using System.Text;
using TCG.Base.Hierarchy;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Rnd.Managers;

public class RandomManager
{
    private Random rnd;
    public int Seed { get; private set; }

    #pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    public RandomManager(int seed)
    {
        Seed = seed;
        ResetRandom();
    }

    public void ResetRandom()
    {
        rnd = new Random(Seed);
    }

    public void ResetRandom(int seed)
    {
        Seed = seed;
        ResetRandom();
    }

    public void RandomizeCanvas(Canvas canvas, bool force = false)
    {
        foreach(var layer in canvas.Layers)
            RandomizeLayer(layer, force);
        foreach(var effect in canvas.Effects)
            RandomizeProperties(effect, force);
    }

    public void RandomizeLayer(Layer layer, bool force = false)
    {
        foreach (var effect in layer.Effects)
            RandomizeProperties(effect, force);

        foreach(var drawable in layer.Drawables)
        {
            RandomizeProperties(drawable, force);

            foreach (var effect in drawable.Effects)
                RandomizeProperties(effect, force);
        }
    }

    public void RandomizeProperties(IRenderable renderable, bool force = false)
    {
        foreach (var property in renderable.GetType().GetProperties())
        {
            if (property.PropertyType.GetInterfaces().Contains(typeof(IRandomizableParameter)))
            {
                Console.WriteLine(renderable.GetType().ToString() + " " + property.Name);
                object? propValue = property.GetValue(renderable);
                if(propValue != null)
                    (propValue as IRandomizableParameter)!.Randomize(rnd, force);
            }
        }
    }
}
