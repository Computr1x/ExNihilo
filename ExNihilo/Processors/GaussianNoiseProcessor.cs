using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace ExNihilo.Processors;

internal class GaussianNoiseProcessor : IImageProcessor
{
    // internal range 0 - 1
    private float _amount = 100f / 400f;
    private Random _rand;

    // public range 0 - 255
    public float Amount
    {
        get => _amount;
        set
        {
            _amount = 1 - (value % 256 / 255f);
        }
    }
    public bool Monochrome { get; set; } = false;


    // public range 0 - 255
    public GaussianNoiseProcessor(int seed = 0, byte amount = 255)
    {
        _rand = new Random(seed);
        Amount = amount;
    }

    public Rectangle Area { get; set; }


    // This is called when we want to build a pixel specific image processor, this is where you get access to the target image to the first time.
    public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        if (Area.Width <= 0 || Area.Height <= 0)
            Area = new Rectangle(0, 0, source.Width, source.Height);

        return new GaussianNoiseProcessorInner<TPixel>(this, source);
    }

    // the main work horse class this has access to the pixel buffer but in an abstract/generic way.
    private class GaussianNoiseProcessorInner<TPixel> : IImageProcessor<TPixel>
        where TPixel : unmanaged, IPixel<TPixel>
    {
        private readonly GaussianNoiseProcessor processor;
        private readonly Image<TPixel> source;

        public GaussianNoiseProcessorInner(GaussianNoiseProcessor processor, Image<TPixel> source)
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

            Rgba32 sourcePixel = new();
            TPixel
                rawPixel = new(),
                resPixel;
            float
                u1 = 0,
                u2 = 0,
                randStdNormal = 0,
                mean = processor.Amount,
                stdDev = (1f - mean) / 6f,
                gaussValue;

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

                        if (processor.Monochrome)
                        {
                            gaussValue = GetNextGaussian(processor._rand, in mean, in stdDev, ref u1, ref u2, ref randStdNormal);
                            sourcePixel.R = (byte) (sourcePixel.R * gaussValue);
                            sourcePixel.G = (byte) (sourcePixel.G * gaussValue);
                            sourcePixel.B = (byte) (sourcePixel.B * gaussValue);
                        }
                        else
                        {
                            sourcePixel.R = (byte) (sourcePixel.R * GetNextGaussian(processor._rand, in mean, in stdDev, ref u1, ref u2, ref randStdNormal));
                            sourcePixel.G = (byte) (sourcePixel.G * GetNextGaussian(processor._rand, in mean, in stdDev, ref u1, ref u2, ref randStdNormal));
                            sourcePixel.B = (byte) (sourcePixel.B * GetNextGaussian(processor._rand, in mean, in stdDev, ref u1, ref u2, ref randStdNormal));
                        }

                        resPixel = new();
                        resPixel.FromRgba32(sourcePixel);
                        pixelRow[x] = resPixel;
                    }
                }
            });
        }

        private static float GetNextGaussian(Random rand, in float mean, in float stdDev, ref float u1, ref float u2, ref float randStdNormal)
        {
            //uniform(0,1] random doubles
            u1 = 1.0f - (float) rand.NextDouble();
            u2 = 1.0f - (float) rand.NextDouble();
            randStdNormal =
                MathF.Sqrt(-2.0f * MathF.Log(u1)) *
                MathF.Sin(2.0f * MathF.PI * u2);
            //random normal (0,1)
            return mean + stdDev * randStdNormal;
        }
    }
}

