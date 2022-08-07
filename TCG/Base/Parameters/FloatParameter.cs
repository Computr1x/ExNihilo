using TCG.Base.Abstract;

namespace TCG.Base.Parameters;

public class FloatParameter : NumericParameter<float>
{
    public FloatParameter(float defaultValue = default) : base(defaultValue) { }

    protected override void RandomizeImplementation(Random r)
    {
        Value = r.NextSingle() * (Max - Min) + Min;
    }
}
