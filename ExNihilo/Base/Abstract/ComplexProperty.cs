using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExNihilo.Base.Interfaces;

namespace ExNihilo.Base.Abstract;

public abstract class ComplexProperty : Property
{
    public override void Randomize(Random r, bool force = false)
    {
        RandomizeImplementation(r);
    }

    protected abstract void RandomizeImplementation(Random r);
}
