﻿using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Visuals;

/// <summary>
/// Define pattern visual object.
/// </summary>
public class Pattern : Visual
{
    /// <summary>
    /// Specifies the rectangular area on which the template will draw.
    /// </summary>
    public RectangleProperty Area { get; } = new RectangleProperty();
    /// <summary>
    /// Defines a rendering template.
    /// </summary>
    public Bool2DArrayProperty Template { get; } = new Bool2DArrayProperty(new bool[,] { { true, false }, { false, true } });
    /// <summary>
    /// Specifies the template color for false values. By the default it's transparent.
    /// </summary>
    public ColorProperty Background { get; } = new ColorProperty(SixLabors.ImageSharp.Color.Transparent);
    /// <summary>
    /// Specifies the template color for true values.
    /// </summary>
    public ColorProperty Foreground { get; } = new ColorProperty(SixLabors.ImageSharp.Color.Black, 5);

    /// <summary>
    /// <inheritdoc cref="Pattern"/>
    /// </summary>
    public Pattern() { }

    /// <summary>
    /// <inheritdoc cref="Pattern"/>
    /// </summary>
    /// <param name="rectangle"><inheritdoc cref="Area" path="/summary"/></param>
    /// <param name="pattern"><inheritdoc cref="Pattern" path="/summary"/></param>
    public Pattern(SixLabors.ImageSharp.Rectangle rectangle, bool[,] pattern)
    {
        Area.WithValue(rectangle);
        Template.Value = pattern;
    }

    /// <summary>
    /// <inheritdoc cref="Pattern"/>
    /// </summary>
    /// <param name="rectangle"><inheritdoc cref="Area" path="/summary"/></param>
    /// <param name="pattern"><inheritdoc cref="Pattern" path="/summary"/></param>
    /// <param name="foreground"><inheritdoc cref="Foreground" path="/summary"/></param>
    public Pattern(SixLabors.ImageSharp.Rectangle rectangle, bool[,] pattern, Color foreground) : this(rectangle, pattern)
    {
        Foreground.Value = foreground;
    }

    /// <summary>
    /// <inheritdoc cref="Pattern"/>
    /// </summary>
    /// <param name="rectangle"><inheritdoc cref="Area" path="/summary"/></param>
    /// <param name="pattern"><inheritdoc cref="Pattern" path="/summary"/></param>
    /// <param name="foreground"><inheritdoc cref="Foreground" path="/summary"/></param>
    /// <param name="background"><inheritdoc cref="Background" path="/summary"/></param>
    public Pattern(SixLabors.ImageSharp.Rectangle rectangle, bool[,] pattern, Color foreground, Color background) : this(rectangle, pattern, foreground)
    {
        Background.Value = background;
    }

    public Pattern WithArea(int x, int y, int width, int height)
    {
        Area.WithValue(new SixLabors.ImageSharp.Rectangle(x, y, width, height));
        return this;
    }

    public Pattern WithArea(SixLabors.ImageSharp.Rectangle value)
    {
        Area.WithValue(value);
        return this;
    }

    /// <summary>
    /// Set area point value.
    /// </summary>
    public Pattern WithPoint(Point p)
    {
        Area.Point.WithValue(p);
        return this;
    }

    /// <summary>
    /// Set point randomization parameters.
    /// </summary>
    public Pattern WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Area.Point.X.WithRandomizedValue(minX, maxX);
        Area.Point.Y.WithRandomizedValue(minY, maxY);
        return this;
    }

    /// <summary>
    /// Set area size value.
    /// </summary>
    public Pattern WithSize(Size size)
    {
        Area.Size.WithValue(size);
        return this;
    }

    /// <summary>
    /// Set size randomization parameters.
    /// </summary>
    public Pattern WithRandomizedSize(int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        Area.Size.Width.WithRandomizedValue(minWidth, maxWidth);
        Area.Size.Height.WithRandomizedValue(minHeight, maxHeight);
        return this;
    }

    /// <summary>
    /// Set area template value.
    /// </summary>
    public Pattern WithTemplate(bool[,] template)
    {
        Template.Value = template;
        return this;
    }
    /// <summary>
    /// Set area template value.
    /// </summary>
    public Pattern WithTemplate(Action<Bool2DArrayProperty> templateSetter)
    {
        templateSetter(Template);
        return this;
    }

    /// <summary>
    /// Set template size randomization parameters.
    /// </summary>
    public Pattern WithRandomizedTemplate(int size)
    {
        Template.Width.Value = Template.Height.Value = size;
        return this;
    }
    /// <summary>
    /// Set area background value.
    /// </summary>
    public Pattern WithBackgroundColor(Color color)
    {
        Background.Value = color;
        return this;
    }
    /// <summary>
    /// Set background color randomization parameters.
    /// </summary>
    /// <param name="colorsCount">Size of generated color palette for background</param>
    /// /// <param name="opacity">Opacity of color</param>
    public Pattern WithRandomizedBackgroundColor(int colorsCount, byte opacity = 255)
    {
        Background.Opacity = opacity;
        Background.Colors = Background.GeneratePalette(colorsCount);
        return this;
    }
    /// <summary>
    /// Set background color randomization parameters.
    /// </summary>
    /// <param name="palette">Color palette for background</param>
    public Pattern WithRandomizedBackgroundColor(Color[] palette)
    {
        Background.Colors = palette;
        return this;
    }
    /// <summary>
    /// Set area foreground value.
    /// </summary>
    public Pattern WithForegroundColor(Color color)
    {
        Foreground.Value = color;
        return this;
    }
    /// <summary>
    /// Set foreground color randomization parameters.
    /// </summary>
    /// <param name="colorsCount">Size of generated color palette for background</param>
    /// <param name="opacity">Opacitity of color</param>
    public Pattern WithRandomizedForegroundColor(int colorsCount, byte opacity = 255)
    {
        Foreground.Opacity = opacity;
        Foreground.Colors = Background.GeneratePalette(colorsCount);
        return this;
    }
    /// <summary>
    /// Set foreground color randomization parameters.
    /// </summary>
    public Pattern WithRandomizedForegroundColor(Color[] palette)
    {
        Foreground.Colors = palette;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        SixLabors.ImageSharp.Rectangle rect = Area;

        if (rect.Width <= 0 || rect.Height <= 0)
            return;

        PatternBrush patternBrush = new(Foreground, Background, Template);
        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };

        image.Mutate(x => x.Fill(dopt, patternBrush, rect));
    }
}
