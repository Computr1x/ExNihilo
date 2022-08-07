using TCG.Base.Abstract;

namespace TCG.Base.Parameters;

public class ByteParameter : NumericParameter<byte>
{
    public ByteParameter(byte defaultValue = default) : base(defaultValue) { }

    protected override void RandomizeImplementation(Random r)
    {
        Value = (byte)r.Next(Min, Max);
    }
}
