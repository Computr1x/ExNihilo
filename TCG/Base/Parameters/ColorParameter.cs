using SixLabors.ImageSharp;
using TCG.Base.Abstract;

namespace TCG.Base.Parameters;

public class ColorParameter : GenericStructParameter<Color>
{
    public Color[] Colors { get; set; }
    public byte Opacity { get; set; }

    public ColorParameter(Color defaultColor, Color[] colors, byte opacity = 255) : base(defaultColor)
    {
        Opacity = opacity;

        Colors = colors.Length > 0 ? colors : GeneratePalette(opacity);
    }

    public ColorParameter(Color defaultColor, int colorsCount = 10, byte opacity = 255) : base(defaultColor)
    {
        Opacity = opacity;
        Colors = GeneratePalette(colorsCount);
    }

    public Color[] GeneratePalette(int colorsCount)
    {
        Color[] colors = new Color[colorsCount];
        float curHue = 0, hueStep = 1f / colorsCount;

        for (int i = 0; i < colorsCount; i++)
        {
            SixLabors.ImageSharp.PixelFormats.Rgba32 color;
            TCG.Base.Utils.ColorsConverter.HsbFToRgb(curHue, 1, 1, out color.R, out color.G, out color.B);
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


