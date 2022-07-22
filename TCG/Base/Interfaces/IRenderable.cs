using SixLabors.ImageSharp;

namespace TCG.Base.Interfaces;

public interface IRenderable
{
    public void Render(Image image, GraphicsOptions graphicsOptions);
}
