using TCG.Base.Abstract;

namespace TCG.Base.Parameters;

public class IntParameter : NumericParameter<int>
{
    public IntParameter(int defaultValue = default) : base(defaultValue) { }

    public IntParameter(int min, int max, int defaultValue = default) : base(min, max, defaultValue) { }

    protected override void RandomizeImplementation(Random r)
    {
        Value = r.Next(Min, Max);
    }
}
