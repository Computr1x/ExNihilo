using SixLabors.ImageSharp;
using ExNihilo.Base.Abstract;

namespace ExNihilo.Base.Properties;

public class ColorProperty : GenericStructProperty<Color>
{
    public Color[] Colors { get; set; }
    public byte Opacity { get; set; }

    public ColorProperty(Color defaultColor, Color[] colors, byte opacity = 255) : base(defaultColor)
    {
        Opacity = opacity;

        Colors = colors.Length > 0 ? colors : GeneratePalette(opacity);
    }

    public ColorProperty(Color defaultColor, int colorsCount = 10, byte opacity = 255) : base(defaultColor)
    {
        Opacity = opacity;
        Colors = GeneratePalette(colorsCount);
    }

    public ColorProperty WithRandomizedValue(Color[] palette)
    {
        Colors = palette;
        return this;
    }

    public Color[] GeneratePalette(int colorsCount)
    {
        Color[] colors = new Color[colorsCount];
        float curHue = 0, hueStep = 1f / colorsCount;

        for (int i = 0; i < colorsCount; i++)
        {
            SixLabors.ImageSharp.PixelFormats.Rgba32 color;
            ExNihilo.Base.Utils.ColorsConverter.HsbFToRgb(curHue, 1, 1, out color.R, out color.G, out color.B);
            color.A = Opacity;
            colors[i] = color;
            curHue += hueStep;
        }
        return colors;
    }

    protected override void RandomizeImplementation(Random r)
    {
        Value = Colors[r.Next(Colors.Length)];
    }
}


