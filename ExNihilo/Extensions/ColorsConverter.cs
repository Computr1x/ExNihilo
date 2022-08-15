namespace ExNihilo.Extensions;

public static class ColorsConverter
{

    /// <summary>Converts HSB color model values to RGB values</summary>
    /// <exception cref="OverflowException">Thrown when any parameter is out of range [0; 1]</exception>
    public static void HsbFToRgb(in float h, in float s, in float v, out float r, out float g, out float b)
    {
        //if (h is < 0 or > 1 || s is < 0 or > 1 || v is < 0 or > 1)
        //	throw new OverflowException();
        float hCopy = h / (1f / 6f);

        var integer = (int) hCopy;
        var fractional = hCopy - integer;

        float
            p = v * (1 - s),
            q = v * (1 - (fractional * s)),
            t = v * (1 - ((1 - fractional) * s));

        // Hue sector to rgb
        switch (integer)
        {
            case 1:
                r = q; g = v; b = p;
                break;
            case 2:
                r = p; g = v; b = t;
                break;
            case 3:
                r = p; g = q; b = v;
                break;
            case 4:
                r = t; g = p; b = v;
                break;
            case 5:
                r = v; g = p; b = q;
                break;
            default:
                r = v; g = t; b = p;
                break;
        }
    }

    public static void HsbFToRgb(in float h, in float s, in float v, out byte r, out byte g, out byte b)
    {
        HsbFToRgb(in h, in s, in v, out float rF, out float gF, out float bF);

        r = (byte) (rF * 255); g = (byte) (gF * 255); b = (byte) (bF * 255);
    }

    /// <summary>Converts HSB color model values to RGB values without ref optimization</summary>
    public static void HsbToRgb(
        in byte h,
        in byte s,
        in byte v,

        out byte r,
        out byte g,
        out byte b
    ) {
        byte region = 0, remainder = 0, p = 0, q = 0, t = 0;

        HsbToRgb(h, s, v, ref region, ref remainder, ref p, ref q, ref t, out r, out g, out b);
    }

    /// <summary>Converts HSB color model values to RGB values with ref optimization</summary>
    public static void HsbToRgb(
        in byte h,
        in byte s,
        in byte v,
        ref byte region,
        ref byte remainder,
        ref byte p,
        ref byte q,
        ref byte t,

        out byte r,
        out byte g,
        out byte b
    ) {

        if (s == 0)
        {
            r = v;
            g = v;
            b = v;
            return;
        }

        region = (byte) (h / 43);
        remainder = (byte)((h - (region * 43)) * 6);

        p = (byte)((v * (255 - s)) >> 8);
        q = (byte)((v * (255 - ((s * remainder) >> 8))) >> 8);
        t = (byte)((v * (255 - ((s * (255 - remainder)) >> 8))) >> 8);

        switch (region)
        {
            case 0:
                r = v; g = t; b = p;
                break;
            case 1:
                r = q; g = v; b = p;
                break;
            case 2:
                r = p; g = v; b = t;
                break;
            case 3:
                r = p; g = q; b = v;
                break;
            case 4:
                r = t; g = p; b = v;
                break;
            default:
                r = v; g = p; b = q;
                break;
        }

        return;
    }

    /// <summary>Converts RGB color model values to HSB values</summary>
    /// <exception cref="OverflowException">Thrown when any parameter is out of range [0; 1]</exception>
    public static void RgbFToHsb(in float r, in float g, in float b, out float h, out float s, out float v)
    {
        //if (r is < 0 or > 1 || g is < 0 or > 1 || b is < 0 or > 1)
        //	throw new OverflowException();

        h = 0;
        v = r > g ? (r > b ? r : b) : (g > b ? g : b);

        var delta = v - (r < g ? (r < b ? r : b) : (g < b ? g : b));

        s = v == 0 ? 0 : delta / v;

        if (s == 0) return;
        // Determining hue sector
        if (r == v)
        {
            h = (g - b) / delta;
        }
        else if (g == v)
        {
            h = 2 + (b - r) / delta;
        }
        else if (b == v)
        {
            h = 4 + (r - g) / delta;
        }

        // Sector to hue
        h *= (1f / 6f);

        // For cases like R = MAX & B > G
        if (h < 0)
            h += 1f;
    }

    /// <summary>Converts RGB color model values to HSB values without ref optimisation</summary>
    public static void RgbToHsb(in byte r, in byte g, in byte b,
        out byte h, out byte s, out byte v)
    {
        byte rgbMin = 0, rgbMax = 0;

        RgbToHsb(r, g, b, ref rgbMin, ref rgbMax, out h, out s, out v);
    }

    /// <summary>Converts RGB color model values to HSB values with ref optimisation</summary>
    public static void RgbToHsb(in byte r, in byte g, in byte b,
        ref byte rgbMin, ref byte rgbMax,
        out byte h, out byte s, out byte v)
    {
        rgbMin = r < g ? (r < b ? r : b) : (g < b ? g : b);
        rgbMax = r > g ? (r > b ? r : b) : (g > b ? g : b);

        v = rgbMax;
        if (v == 0)
        {
            h = 0;
            s = 0;
            return;
        }

        s = (byte) (255 * (rgbMax - rgbMin) / v);
        if (s == 0)
        {
            h = 0;
            return;
        }

        if (rgbMax == r)
            h = (byte) (0 + 43 * (g - b) / (rgbMax - rgbMin));
        else if (rgbMax == g)
            h = (byte) (85 + 43 * (b - r) / (rgbMax - rgbMin));
        else
            h = (byte) (171 + 43 * (r - g) / (rgbMax - rgbMin));

        return;
    }
}
