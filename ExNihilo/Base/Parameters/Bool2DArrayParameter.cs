using ExNihilo.Base.Abstract;

namespace ExNihilo.Base.Properties;

public class Bool2DArrayProperty : GenericProperty<bool[,]>
{
    public IntProperty Width { get; set; } = new IntProperty(0, int.MaxValue, 2) { Min = 2, Max = 2 };
    public IntProperty Height { get; set; } = new IntProperty(0, int.MaxValue, 2) { Min = 2, Max = 2 };

    public Bool2DArrayProperty(bool[,] defaultValue) : base(defaultValue)
    {
    }

    public Bool2DArrayProperty WithSize(int size)
    {
        Width.Value = Height.Value = size;
        return this;
    }

    public Bool2DArrayProperty WithRandomizedSize(int min, int max)
    {
        Width.WithRandomizedValue(min, max);
        Width.WithRandomizedValue(min, max);
        return this;
    }

    protected override void RandomizeImplementation(Random r)
    {
        Width.Randomize(r);
        Height.Randomize(r);

        Value = new bool[Width, Height];
        for (int i = 0; i < (int) Width; i++)
        {
            for (int j = 0; j < (int) Height; j++)
            {
                Value[i, j] = r.NextSingle() > 0.5f;
            }
        }
    }
}
