using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExNihilo.Base.Abstract;

namespace ExNihilo.Base.Parameters;

public class EnumParameter<T> : GenericStructParameter<T> where T : struct 
{
    public T[] EnumValues { get; set; }

    public EnumParameter(T defaultValue = default) : base(defaultValue)
    {
        EnumValues = (T[])Enum.GetValues(typeof(T));
    }

    public EnumParameter(List<T> enumValues, T defaultValue = default) : base(defaultValue)
    {
        EnumValues = enumValues.ToArray();
    }

    public EnumParameter<T> WithRandomizedValue(IEnumerable<T> values)
    {
        EnumValues = values.ToArray();
        return this;
    }

    protected override void RandomizeImplementation(Random r)
    {
        Value = EnumValues[r.Next(EnumValues.Length)];
    }
}