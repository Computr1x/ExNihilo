using ExNihilo.Base.Abstract;

namespace ExNihilo.Base.Parameters;

public class Bool2DArrayParameter : GenericParameter<bool[,]>
{
    public IntParameter Width { get; set; } = new IntParameter(0, int.MaxValue, 2) { Min = 2, Max = 2 };
    public IntParameter Height { get; set; } = new IntParameter(0, int.MaxValue, 2) { Min = 2, Max = 2 };

    public Bool2DArrayParameter(bool[,] defaultValue) : base(defaultValue)
    {
    }

    public Bool2DArrayParameter WithSize(int size)
    {
        Width.Value = Height.Value = size;
        return this;
    }

    public Bool2DArrayParameter WithRandomizedSize(int min, int max)
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
        for (int i = 0; i < (int)Width; i++)
        {
            for (int j = 0; j < (int)Height; j++)
            {
                Value[i, j] = r.NextSingle() > 0.5f;
            }
        }
    }
}
