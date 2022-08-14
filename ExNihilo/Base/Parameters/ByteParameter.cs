using ExNihilo.Base.Abstract;

namespace ExNihilo.Base.Parameters;

public class ByteParameter : NumericParameter<byte>
{
    public ByteParameter(byte defaultValue = default) : base(defaultValue) { }

    public ByteParameter(byte min, byte max, byte defaultValue = default) : base(min, max, defaultValue) { }

    protected override void RandomizeImplementation(Random r)
    {
        Value = (byte)r.Next(Min, Max);
    }
}
