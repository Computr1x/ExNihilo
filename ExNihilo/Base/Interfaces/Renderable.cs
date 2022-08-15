using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ExNihilo.Base.Interfaces;

public abstract class Renderable
{
    public virtual void Render(Image image, GraphicsOptions graphicsOptions)
    {

    }
}
