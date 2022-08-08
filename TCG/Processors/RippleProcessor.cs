using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace TCG.Processors;

internal class RippleProcessor : IImageProcessor
{
    private bool customCoords = false;

    public Rectangle Area { get; set; }

    // center of Ripple, by default center of image
    public int X { get; set; }
    public int Y { get; set; }
    // radius of effect in pixels
    public float Radius { get; } = 100f;
    //  wavelength of ripples, in pixels
    public float WaveLength { get; } = 10f;
    // approximate width of wave train, in wavelengths
    public float TraintWidth { get; set; } = 2f;

    public RippleProcessor()
    {
    }

    public RippleProcessor(int x, int y)
    {
        X = x;
        Y = y;
        customCoords = true;
    }

    public RippleProcessor(float radius, float waveLength)
    {
        Radius = radius;
        WaveLength = waveLength;
    }

    public RippleProcessor(float radius, float waveLength, float traintWidth)
    {
        Radius = radius;
        WaveLength = waveLength;
        TraintWidth = traintWidth;
    }

    public RippleProcessor(int x, int y, float radius, float waveLength, float traintWidth) : this(x, y)
    {
        Radius = radius;
        WaveLength = waveLength;
        TraintWidth = traintWidth;
    }

    public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        if (Area.Width <= 0 || Area.Height <= 0)
            Area = new Rectangle(0, 0, source.Width, source.Height);

        return new RippleProcessorInner<TPixel>(this, source);
    }

    private class RippleProcessorInner<TPixel> : IImageProcessor<TPixel>
        where TPixel : unmanaged, IPixel<TPixel>
    {
        private RippleProcessor processor;
        private Image<TPixel> source;

        public RippleProcessorInner(RippleProcessor processor, Image<TPixel> source)
        {
            this.processor = processor;
            this.source = source;
        }

        public void Dispose()
        {
        }

        public void Execute()
        {
            var workArea = processor.Area;

            // create copy of image
            TPixel[] imageCopyArray = new TPixel[source.Width * source.Height];
            source.CopyPixelDataTo(imageCopyArray);


            // calculate work area
            int imageWidth = source.Width, imageHeight = source.Height;

            int width = Math.Min(workArea.Width, source.Width), height = Math.Min(workArea.Height, source.Height);
            if (workArea.X + width > source.Width)
                width = source.Width - workArea.X;
            if (workArea.Y + height > source.Height)
                height = source.Height - workArea.Y;

            // init vars
            float centerX = width / 2.0f, centerY = height / 2.0f,
                pixelX = 0, pixelY = 0, r = 0, z = 0,
                offsetX = 0, offsetY = 0;
            int intOffsetX = 0, intOffsetY = 0;
            Rgba32 sourcePixel = new();

            if (processor.customCoords)
            {
                centerX = processor.X;
                centerY = processor.Y;
            }


            source.ProcessPixelRows(accessor =>
            {
                for (int y = workArea.Y; y < height + workArea.Y; y++)
                {
                    Span<TPixel> pixelRow = accessor.GetRowSpan(y);

                    for (int x = workArea.X; x < width + workArea.X; x++)
                    {
                        CalculateRipple(in x, in y,
                                in centerX, in centerY,
                                ref pixelX, ref pixelY,
                                ref r, ref z,
                                ref offsetX, ref offsetY);


                        intOffsetX = (int)offsetX;
                        intOffsetY = (int)offsetY;

                        if (0 <= intOffsetX && intOffsetX < imageWidth && 0 <= intOffsetY && intOffsetY < imageHeight)
                        {
                            TPixel resPixel = imageCopyArray[intOffsetY * source.Width + intOffsetX];
                            resPixel.ToRgba32(ref sourcePixel);
                            pixelRow[x].FromRgba32(sourcePixel);
                        }
                    }
                }
            });
        }

        private void CalculateRipple(in int x, in int y,
                in float centerX, in float centerY,
                ref float pixelX, ref float pixelY,
                ref float r, ref float z,
                ref float offsetX, ref float offsetY)
        {
            pixelX = x - centerX;
            pixelY = y - centerY;

            r = (MathF.Sqrt(pixelX * pixelX + pixelY * pixelY) - processor.Radius) / processor.WaveLength;
            z = 1f / (1f + MathF.Pow(r / processor.TraintWidth, 2f)) * MathF.Sin(r * 2f * MathF.PI);

            offsetX = x + x * z;
            offsetY = y + y * z;
        }
    }
}

