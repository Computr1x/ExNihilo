using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using System.Numerics;
using System.Text;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;
using TCG.Rnd.Range;

namespace TCG.Rnd.Managers;

public class RNDManager
{
    private Random rnd;
    public int Seed { get; private set; }

    public RNDManager(int seed)
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


    
    public void RandomizeProperties(IRenderable renderable)
    {
        foreach (var property in GetType().GetProperties())
        {
            if (property.GetValue(renderable) is IRandomizableParameter parameter)
                parameter.Randomize(rnd);
            if (property.PropertyType.IsArray && property.GetType().GetElementType() is IRandomizableParameter)
            {
                Array a = (Array)property.GetValue(renderable);
                foreach(var arrProperty in a)
                {
                    (arrProperty as IRandomizableParameter).Randomize(rnd);
                }
            }
        }
    }
}
