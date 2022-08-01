using TCG.Base.Abstract;
using TCG.Base.Interfaces;

namespace TCG.Base.Parameters;

public class IntParameter : GenericStructParameter<int>, IHasMinMax<int>
{
    public int Max { get; set; }

    public int Min { get; set; }

    public IntParameter(int defaultValue = default) : base(defaultValue) { }

    protected override void RandomizeImplementation(Random r)
    {
        Value = r.Next(Min, Max);
    }
}
