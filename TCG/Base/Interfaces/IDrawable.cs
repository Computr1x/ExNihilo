using SixLabors.ImageSharp.Drawing.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Base.Interfaces;

public interface IDrawable : IRenderable
{
    public IList<IEffect> Effects { get; }

    //public override void Render(Image image, GraphicsOptions graphicsOptions);
    //public void Render(Image image);
}
