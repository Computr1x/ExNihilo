using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Abstract;

namespace TCG.Drawables;

public class DEllipse : BaseDrawable
{
    public Rectangle Rect { get; set; }

    public DEllipse(Rectangle rect) : base()
    {
        Rect = rect;
    }

    public DEllipse(int x, int y, int width, int height)
    : this(new Rectangle(x, y, width, height))
    { }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        IPath path = new EllipsePolygon(Rect.X, Rect.Y, Rect.Width, Rect.Height);
        DrawingOptions dopt = new DrawingOptions() { GraphicsOptions = graphicsOptions };

        
        image.Mutate((x) =>
        {
            if(Type.HasFlag(DrawableType.Filled))
                x.Fill(dopt, Brush, path);
            if (Type.HasFlag(DrawableType.Outlined))
                x.Draw(dopt, Pen, path);
        });
    }
}
