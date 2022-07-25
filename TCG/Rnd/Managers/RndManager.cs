using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TCG.Rnd.Range;

namespace TCG.Rnd.Managers;

public class RNDManager
{
    private Random rnd;
    public int Seed { get; private set; }

    //public record RNDIntRange(int low, int Max);
    // public record RNDSymbolsRange(char[] symbols);
    // public record RNDRectRange(float x, float y, float width, float height);
    // public record RNDRectRange(float x, float y, float width, float height);

    public RNDManager(int seed)
    {
        Seed = seed;
        ResetRandom();
    }

    public void ResetRandom()
    {
        rnd = new Random(Seed);
    }

    public void ResetRandom(int seed)
    {
        Seed = seed;
        ResetRandom();
    }

    public bool NextBool()
    {
        return rnd.NextSingle() >= 0.5f;
    }

    public float NextFloat(BasicRange<float> range)
    {
        int ceilPart = rnd.Next((int)range.Min, (int)range.Max);
        return ceilPart + rnd.NextSingle();
    }

    public float NextFloat()
    {
        return rnd.NextSingle();
    }

    public float NextFloat(float max)
    {
        return NextFloat(new BasicRange<float>(0, max));
    }

    public int NextInt(BasicRange<int> range)
    {
        return rnd.Next(range.Min, range.Max);
    }

    public int NextInt(int max)
    {
        return rnd.Next(0, max);
    }

    public byte NextByte(BasicRange<byte> range)
    {
        return (byte)rnd.Next(range.Min, range.Max);
    }

    public byte NextByte()
    {
        return (byte)rnd.Next(0, 256);
    }


    public Vector2 NextPoint(RectangleRange rect)
    {
        return new Vector2(
            NextFloat(new BasicRange<float>(rect.Left, rect.Right)),
            NextFloat(new BasicRange<float>(rect.Top, rect.Bottom)));
    }

    public Vector2 NextPoint(CircleRange circle)
    {
        return new Vector2(
            circle.X + circle.Radius * MathF.Sqrt(rnd.NextSingle()),
            circle.Y + circle.Radius * MathF.Sqrt(rnd.NextSingle())
        );
    }

    public Size NextSize(SizeRange<int> sizeRange)
    {
        return NextSize(sizeRange.widthRange, sizeRange.heightRange);
    }

    public Size NextSize(BasicRange<int> widthRange, BasicRange<int> hightRange)
    {
        return new Size(NextInt(widthRange), NextInt(hightRange));
    }

    public Color NextColor(byte alpha = 255)
    {
        var color = new Rgba32((uint)rnd.Next())
        {
            A = alpha
        };
        return new Color(color);
    }

    public Color NextColor(ColorRange colorRange)
    {
        return colorRange.Colors[rnd.Next(0, colorRange.Colors.Length)];
    }


    public IBrush NextBrush(ColorRange colorRange = null)
    {
        if(colorRange == null)
            colorRange = new ColorRange();
        Color color = NextColor(colorRange);
        
        return Brushes.Solid(color);
    }

    public IPen NextPen(Base.Utils.PenType penType = Base.Utils.PenType.Solid, ColorRange colorRange = null, BasicRange<int> widthRange = null)
    {
        if (colorRange == null)
            colorRange = new ColorRange();
        if (widthRange == null)
            widthRange = new BasicRange<int>(1);

        Color color = NextColor(colorRange);
        int width = NextInt(widthRange);

        return penType switch
        {
            Base.Utils.PenType.Dot => Pens.Dot(color, width),
            Base.Utils.PenType.Dash => Pens.Dash(color, width),
            Base.Utils.PenType.DashDot => Pens.DashDot(color, width),
            Base.Utils.PenType.DashDotDot => Pens.DashDotDot(color, width),
            _ => Pens.Solid(color, width),
        };
    }

    public string NextText(TextRange textRange)
    {
        if (textRange.GenerateMode)
        {
            int textLength = NextInt(textRange.TextLengthRange);

            StringBuilder sb = new();
            for (int i = 0; i < textLength; i++)
                sb.Append(
                    textRange.Chars[rnd.Next(0, textRange.Chars.Length)]);

            return sb.ToString();
        }
        else
        {
            return textRange.Words[NextInt(textRange.Words.Length)];
        }
    }

    public T NextEnum<T>(EnumRange<T> enumRange) where T : struct
    {
        return enumRange.EnumValues[NextInt(enumRange.EnumValues.Length)];
    }
}
