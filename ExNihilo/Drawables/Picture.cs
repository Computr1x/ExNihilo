﻿using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Abstract;
using ExNihilo.Base.Parameters;

namespace ExNihilo.Drawables;

/// <summary>
/// Define picture drawable object.
/// </summary>
public class Picture : BaseDrawable
{
    /// <summary>
    /// Specifies the coordinate of the upper left corner of the image from which to start rendering
    /// </summary>
    public PointParameter Point { get; } = new PointParameter();

    private Image? image = null;

    /// <summary>
    /// <inheritdoc cref="Picture"/>
    /// </summary>
    /// <param name="path">Path to image location</param>
    public Picture(string path)
    {
        image = Image.Load(path);
    }

    /// <summary>
    /// <inheritdoc cref="Picture"/>
    /// </summary>
    /// <param name="image">Image value in bytes</param>
    public Picture(byte[] image)
    {
        this.image = Image.Load(image);
    }

    /// <summary>
    /// <inheritdoc cref="Picture"/>
    /// </summary>
    /// <param name="image"><inheritdoc cref="Image" path="/summary"/></param>
    public Picture(Image image)
    {
        this.image = image;
    }

    /// <summary>
    /// Set area point value.
    /// </summary>
    public Picture WithPoint(Point p)
    {
        Point.WithValue(p);
        return this;
    }
    /// <summary>
    /// Set point randomization parameters.
    /// </summary>
    public Picture WithRandomiPoint(Point p)
    {
        Point.WithValue(p);
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        if (this.image == null)
            return;

        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };

        image.Mutate((x) =>
        {
            x.DrawImage(this.image, Point, graphicsOptions);
        });
    }
}