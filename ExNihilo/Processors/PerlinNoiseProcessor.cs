﻿using ExNihilo.Extensions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;
using System.Numerics;

namespace ExNihilo.Processors;

internal class PerlinNoiseProcessor : IImageProcessor
{
    private readonly byte[] permutationTable;
    private float _amount;

    public int Step { get; set; } = 1;

    public bool Monochrome { get; set; } = false;

    public int Octaves { get; set; } = 5;

    // public range 0 - 255
    public float Amount
    {
        get => _amount;
        set
        {
            _amount = 1 - (value % 256 / 255f);
        }
    }

    public float Persistence { get; set; } = 0.5f;

    public Rectangle Area { get; set; }

    public PerlinNoiseProcessor(int seed = 0, byte amount = 255)
    {
        Amount = amount;
        var rand = new Random(seed);
        permutationTable = new byte[1024];
        rand.NextBytes(permutationTable);
    }

    public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        if (Area.Width <= 0 || Area.Height <= 0)
            Area = new Rectangle(0, 0, source.Width, source.Height);

        return new PerlinNoiseProcessorInner<TPixel>(this, source);
    }

    private class PerlinNoiseProcessorInner<TPixel> : IImageProcessor<TPixel>
        where TPixel : unmanaged, IPixel<TPixel>
    {
        private readonly PerlinNoiseProcessor processor;
        private readonly Image<TPixel> source;

        public PerlinNoiseProcessorInner(PerlinNoiseProcessor processor, Image<TPixel> source)
        {
            this.processor = processor;
            this.source = source;
        }

        public void Dispose()
        {
        }

        public void Execute()
        {
            // calc work area
            var workArea = processor.Area;
            int imageWidth = source.Width, imageHeight = source.Height;

            int width = Math.Min(workArea.Width, source.Width), height = Math.Min(workArea.Height, source.Height);
            if (workArea.X + width > source.Width)
                width = source.Width - workArea.X;
            if (workArea.Y + height > source.Height)
                height = source.Height - workArea.Y;

            // init vars
            Rgba32 sourcePixel = new();
            TPixel rawPixel = new();
            float noise = 0;
            float opacity = processor.Amount;

            source.ProcessPixelRows(accessor =>
            {
                for (int y = workArea.Y; y < height + workArea.Y; y++)
                {
                    Span<TPixel> pixelRow = accessor.GetRowSpan(y);

                    for (int x = workArea.X; x < width + workArea.X; x++)
                    {
                        pixelRow[x].ToRgba32(ref sourcePixel);

                        if (sourcePixel.A == 0)
                            continue;

                        noise = Noise(x / (float)width, y / (float)height, processor.Octaves, processor.Persistence);
                        if (processor.Monochrome)
                        {
                            
                            sourcePixel.R = (byte)(sourcePixel.R * noise);
                            sourcePixel.G = (byte)(sourcePixel.G * noise);
                            sourcePixel.B = (byte)(sourcePixel.B * noise);
                        }
                        else
                        {
                            ColorsConverter.HsbFToRgb(noise, 1f, 1f, out float r, out float g, out float b);
                            sourcePixel.R = (byte)(r * sourcePixel.R);
                            sourcePixel.G = (byte)(g * sourcePixel.G);
                            sourcePixel.B = (byte)(b * sourcePixel.B);
                        }

                        sourcePixel.A = (byte)(sourcePixel.A * opacity);

                        TPixel resPixel = new();
                        resPixel.FromRgba32(sourcePixel);
                        pixelRow[x] = resPixel;
                    }
                }
            });
        }

        private Vector2 GetPseudoRandomGradientVector(int x, int y)
        {
            int v = processor.permutationTable[(int)(((x * 1836311903) ^ (y * 2971215073) + 4807526976) & 1023)] & 3;

            return v switch
            {
                0 => new Vector2(1, 0),
                1 => new Vector2(-1, 0),
                2 => new Vector2(0, 1),
                _ => new Vector2(0, -1)
            };
        }

        static float QunticCurve(float t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        static float Dot(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        private float Noise(float fx, float fy)
        {
            int left = (int)MathF.Floor(fx);
            int top = (int)MathF.Floor(fy);
            float pointInQuadX = fx - left;
            float pointInQuadY = fy - top;

            var topLeftGradient = GetPseudoRandomGradientVector(left, top);
            var topRightGradient = GetPseudoRandomGradientVector(left + 1, top);
            var bottomLeftGradient = GetPseudoRandomGradientVector(left, top + 1);
            var bottomRightGradient = GetPseudoRandomGradientVector(left + 1, top + 1);

            var distanceToTopLeft = new Vector2(pointInQuadX, pointInQuadY);
            var distanceToTopRight = new Vector2(pointInQuadX - 1, pointInQuadY);
            var distanceToBottomLeft = new Vector2(pointInQuadX, pointInQuadY - 1);
            var distanceToBottomRight = new Vector2(pointInQuadX - 1, pointInQuadY - 1);

            float tx1 = Dot(distanceToTopLeft, topLeftGradient);
            float tx2 = Dot(distanceToTopRight, topRightGradient);
            float bx1 = Dot(distanceToBottomLeft, bottomLeftGradient);
            float bx2 = Dot(distanceToBottomRight, bottomRightGradient);

            pointInQuadX = QunticCurve(pointInQuadX);
            pointInQuadY = QunticCurve(pointInQuadY);

            float tx = Lerp(tx1, tx2, pointInQuadX);
            float bx = Lerp(bx1, bx2, pointInQuadX);
            float tb = Lerp(tx, bx, pointInQuadY);

            return tb;
        }

        private float Noise(float fx, float fy, int octaves, float persistence = 0.5f)
        {
            float amplitude = 1;
            float max = 0;
            float result = 0;

            while (octaves-- > 0)
            {
                max += amplitude;
                result += Noise(fx, fy) * amplitude;
                amplitude *= persistence;
                fx *= 2;
                fy *= 2;
            }

            return result / max;
        }
    }
}

