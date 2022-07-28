using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Base.Interfaces;

public interface IHasMinMax<T>
{
    public T Min { get; set; }
    public T Max { get; set; }
}
