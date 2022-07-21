using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Interfaces;

namespace TCG.Drawables
{
    internal class Test : IDrawable
    {
        public IList<IEffect> Effects => throw new NotImplementedException();

        public void Render(Image image, GraphicsOptions graphicsOptions)
        {
            throw new NotImplementedException();
        }
    }
}
