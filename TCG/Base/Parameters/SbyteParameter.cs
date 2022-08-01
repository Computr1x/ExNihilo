using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Abstract;
using TCG.Base.Interfaces;

namespace TCG.Base.Parameters;

public class SbyteParameter : GenericStructParameter<sbyte>, IHasMinMax<sbyte>
{
    public sbyte Max { get; set; }

    public sbyte Min { get; set; }

    public SbyteParameter(sbyte defaultValue) : base(defaultValue)
    {
    }
    
    protected override void RandomizeImplementation(Random r)
    {
        Value = (sbyte)r.Next(Min, Max);
    }
}