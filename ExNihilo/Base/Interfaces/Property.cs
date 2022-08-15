using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExNihilo.Base.Interfaces;

public abstract class Property
{
    public abstract void Randomize(Random r, bool force = false);
}
