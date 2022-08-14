using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Interfaces;

namespace TCG.Base.Abstract;

public abstract class ComplexParameter : IRandomizableParameter
{
    public void Randomize(Random r, bool force = false)
    {
        RandomizeImplementation(r);
    }
    protected abstract void RandomizeImplementation(Random r);
}
