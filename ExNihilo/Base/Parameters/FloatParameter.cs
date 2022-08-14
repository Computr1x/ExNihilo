using ExNihilo.Base.Abstract;

namespace ExNihilo.Base.Parameters;

public class FloatParameter : NumericParameter<float>
{
    public FloatParameter(float defaultValue = default) : base(defaultValue) { }

    public FloatParameter(float min, float max, float defaultValue = default) : base(min, max, defaultValue) { }

    protected override void RandomizeImplementation(Random r)
    {
        Value = r.NextSingle() * (Max - Min) + Min;
    }
}
