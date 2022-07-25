using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Rnd.Range;

public class RectangleRange
{    
    public float Left { get; }
    public float Top { get; }
    public float Right { get; }
    public float Bottom { get; }

    public RectangleRange(float left, float top, float right, float bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public static explicit operator RectangleRange(Rectangle rect) =>
        new(rect.Left, rect.Top, rect.Right, rect.Bottom);
    //public static explicit operator RectangleRange(Size rect) =>
    //    new(rect., rect.Top, rect.Right, rect.Bottom);
    //public static explicit operator RectangleRange( rect) =>
    //    new(0, 0, rect.Size.Width, rect.Size.Height);
}