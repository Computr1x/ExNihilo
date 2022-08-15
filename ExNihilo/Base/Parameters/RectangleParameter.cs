﻿using SixLabors.ImageSharp;
using ExNihilo.Base.Abstract;

namespace ExNihilo.Base.Parameters;

public class RectangleParameter : ComplexParameter
{
    public PointParameter Point { get; } = new PointParameter();
    public SizeParameter Size { get; } = new SizeParameter();

    public RectangleParameter WithValue(Rectangle value)
    {
        Point.WithValue(new Point(value.X, value.Y));
        Size.WithValue(new Size(value.Width, value.Height));
        return this;
    }

    public RectangleParameter WithPoint(Point p)
    {
        Point.WithValue(p);
        return this;
    }

    public RectangleParameter WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Point.WithRandomizedValue(minX, maxX, minY, maxY);
        return this;
    }

    public RectangleParameter WithSize(Size size)
    {
        Size.WithValue(size);
        return this;
    }

    public RectangleParameter WithRandomizedSize(int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        Size.WithRandomizedValue(minWidth, maxWidth, minHeight, maxHeight);
        return this;
    }

    protected override void RandomizeImplementation(Random r)
    {
        Point.Randomize(r);
        Size.Randomize(r);
    }

    public static implicit operator RectangleF(RectangleParameter rectParamater)
    {
        return new RectangleF(rectParamater.Point, rectParamater.Size);
    }

    public static implicit operator Rectangle(RectangleParameter rectParamater)
    {
        return new Rectangle(rectParamater.Point, rectParamater.Size);
    }
}
