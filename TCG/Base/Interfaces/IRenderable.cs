using SixLabors.ImageSharp.Drawing.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Base.Interfaces;

public interface IRenderable
{
    public void Render(Image image, GraphicsOptions graphicsOptions);
}
