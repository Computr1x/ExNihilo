using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Interfaces;

namespace TCG.Base.Abstract;

public abstract class BaseComplexDrawable : BaseDrawable, IComplexDrawable
{
    public IList<IDrawable> Drawables { get; } = new List<IDrawable>();

    public BaseComplexDrawable WithDrawable(IDrawable drawable)
    {
        Drawables.Add(drawable);
        return this;
    }
}
