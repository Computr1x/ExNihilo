using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Utils;

namespace TCG.Rnd.Range;

public class ColorRange
{
    public Color[] Colors { get; }
    public byte Opacity { get; }

    public ColorRange(Color[] colors, byte opacity = 255)
    {
        Colors = colors;
        Opacity = opacity;
    }

    public ColorRange(byte opacity = 255)
    {
        Colors = GeneratePalette(255);
        Opacity = opacity;
    }

    public ColorRange(int colorsCount, byte opacity = 255)
    {
        Colors = GeneratePalette(colorsCount);
        Opacity = opacity;
    }

    private Color[] GeneratePalette(int colorsCount)
    {
        Color[] colors = new Color[colorsCount];
        float curHue = 0, hueStep = 255f / colorsCount;

        for (int i = 0; i < colorsCount; i++)
        {
            Rgba32 color;
            ColorsConverter.HsbFToRgb(curHue, 1, 1, out color.R, out color.G, out color.B);
            color.A = Opacity;
            colors[i] = color;
            curHue += hueStep;
        }
        return colors;
    }
}
