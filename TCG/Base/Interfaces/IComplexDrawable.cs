using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Base.Interfaces;

public interface IComplexDrawable : IDrawable
{
    public IList<IDrawable> Drawables { get; }
}