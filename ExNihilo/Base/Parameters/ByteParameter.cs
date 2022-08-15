using ExNihilo.Base.Abstract;

namespace ExNihilo.Base.Properties;

public class ByteProperty : NumericProperty<byte>
{
    public ByteProperty(byte defaultValue = default) : base(defaultValue) { }

    public ByteProperty(byte min, byte max, byte defaultValue = default) : base(min, max, defaultValue) { }

    protected override void RandomizeImplementation(Random r)
    {
        Value = (byte) r.Next(Min, Max);
    }
}
