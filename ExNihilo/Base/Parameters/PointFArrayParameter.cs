using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExNihilo.Base.Abstract;
using ExNihilo.Base.Properties;

namespace ExNihilo.Base.Properties;

public class PointFArrayProperty : GenericProperty<PointF[]>
{
    public IntProperty Length { get; } = new IntProperty(0);

    public IntProperty X { get; } = new IntProperty(0);
    public IntProperty Y { get;  } = new IntProperty(0);

    public PointFArrayProperty(PointF[] defaultValue) : base(defaultValue)
    {
    }

    public PointFArrayProperty WithLength(int value)
    {
        Length.WithValue(value);
        return this;
    }

    public PointFArrayProperty WithRadnomizeLength(int min, int max)
    {
        Length.WithRandomizedValue(min, max);
        return this;
    }

    protected override void RandomizeImplementation(Random r)
    {
        Length.Randomize(r);
        
        Value = new PointF[Length];
        for (int i = 0; i < Length; i++)
        {
            X.Randomize(r);
            Y.Randomize(r);
            Value[i] = new PointF(X, Y);
        }
    }
}
