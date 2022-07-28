using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Abstract;

namespace TCG.Rnd.Randomizers.Parameters;

public class BoolParameter : GenericStructParameter<bool>
{
    public BoolParameter(bool defaultValue) : base(defaultValue)
    {
    }

    protected override void RandomizeImplementation(Random r)
    {
        Value = r.NextSingle() > 0.5f;
    }
}