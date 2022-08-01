using TCG.Base.Abstract;
using TCG.Base.Interfaces;

namespace TCG.Base.Parameters;

public class ByteParameter : GenericStructParameter<byte>, IHasMinMax<byte>
{
    public byte Max { get; set; }

    public byte Min { get; set; }

    public ByteParameter(byte defaultValue = default(byte)) : base(defaultValue) { }

    protected override void RandomizeImplementation(Random r)
    {
        Value = (byte)r.Next(Min, Max);
    }
}
