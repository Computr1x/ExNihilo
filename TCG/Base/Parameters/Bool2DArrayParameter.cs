using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Abstract;

namespace TCG.Base.Parameters;

public class Bool2DArrayParameter : Array2DParameter<bool>
{
    public Bool2DArrayParameter(bool[,] defaultValue) : base(defaultValue)
    {
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
