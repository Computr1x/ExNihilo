using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Rnd.Range;

public class EnumRange<T> where T : struct
{
    public T[] EnumValues { get; set; }

    public EnumRange()
    {
        EnumValues = (T[])Enum.GetValues(typeof(T));
    }

    public EnumRange(List<T> enumValues)
    {
        EnumValues = enumValues.ToArray();
    }
}