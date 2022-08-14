using SixLabors.ImageSharp;

namespace ExNihilo.Base.Interfaces;

public interface IRenderable
{
    public void Render(Image image, GraphicsOptions graphicsOptions);
}
