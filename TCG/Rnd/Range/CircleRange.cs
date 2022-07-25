using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Rnd.Range;

public class CircleRange
{
    public float X { get; }
    public float Y { get; }
    public float Radius { get; }

    public CircleRange(float x, float y, float radius)
    {
        X = x;
        Y = y;
        Radius = radius;
    }

    public CircleRange(Vector2 point, float radius)
    {
        this.X = point.X;
        this.Y = point.Y;
        Radius = radius;
    }
}