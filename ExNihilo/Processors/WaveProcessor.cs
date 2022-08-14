using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace ExNihilo.Processors;

public enum WaveType { Sine, Triangle, Square };

internal class WaveProcessor : IImageProcessor
{
    public Rectangle Area { get; set; }

    public float WaveLength { get; }
    public float Amplitude { get; }
    public WaveType WaveType { get; } = WaveType.Sine;

    public WaveProcessor(float waveLength, float amplitude)
    {
        WaveLength = waveLength;
        Amplitude = amplitude;
    }


    public WaveProcessor(float waveLength, float amplitude, WaveType waveType) : this(waveLength, amplitude)
    {
        WaveType = waveType;
    }

    public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        if (Area.Width <= 0 || Area.Height <= 0)
            Area = new Rectangle(0, 0, source.Width, source.Height);

        return new WaveProcessorInner<TPixel>(this, source);
    }

    private class WaveProcessorInner<TPixel> : IImageProcessor<TPixel>
        where TPixel : unmanaged, IPixel<TPixel>
    {
        private WaveProcessor processor;
        private Image<TPixel> source;

        private delegate void WaveDeleagate(in int x, in int y, ref float pixelX, ref float pixelY);

        public WaveProcessorInner(WaveProcessor processor, Image<TPixel> source)
        {
            this.processor = processor;
            this.source = source;
        }

        public void Dispose()
        {
        }

        public void Execute()
        {
            // calculate work area
            var workArea = processor.Area;
            int imageWidth = source.Width, imageHeight = source.Height;

            int width = Math.Min(workArea.Width, source.Width), height = Math.Min(workArea.Height, source.Height);
            if (workArea.X + width > source.Width)
                width = source.Width - workArea.X;
            if (workArea.Y + height > source.Height)
                height = source.Height - workArea.Y;

            // create copy of image
            TPixel[] imageCopyArray = new TPixel[source.Width * source.Height];
            source.CopyPixelDataTo(imageCopyArray);


            // init vars
            float pixelX = 0, pixelY = 0, offsetX = 0, offsetY = 0;
            int intOffsetX = 0, intOffsetY = 0;
            Rgba32 sourcePixel = new();

            WaveDeleagate calcWave = processor.WaveType switch
            {
                WaveType.Sine => SineWave,
                WaveType.Triangle => TriangleWave,
                _ => SquareWave,
            };


            source.ProcessPixelRows(accessor =>
            {
                for (int y = workArea.Y; y < height + workArea.Y; y++)
                {
                    Span<TPixel> pixelRow = accessor.GetRowSpan(y);

                    for (int x = workArea.X; x < width + workArea.X; x++)
                    {
                        CalculateWave(in x, in y, ref pixelX, ref pixelY, ref offsetX, ref offsetY, ref calcWave);

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

        private void CalculateWave(in int x, in int y,
                ref float pixelX, ref float pixelY,
                ref float xOffset, ref float yOffset,
                ref WaveDeleagate calcWave)
        {
            calcWave(in x, in y, ref pixelX, ref pixelY);

            xOffset = x + pixelX;
            yOffset = y + pixelY;
        }

        private void SineWave(in int x, in int y,
                ref float pixelX, ref float pixelY)
        {
            pixelX = processor.Amplitude * MathF.Sin(2.0f * MathF.PI * y / processor.WaveLength);
            pixelY = processor.Amplitude * MathF.Cos(2.0f * MathF.PI * x / processor.WaveLength);
        }

        private void TriangleWave(in int x, in int y,
            ref float pixelX, ref float pixelY)
        {
            pixelX = 2f * processor.Amplitude / MathF.PI * MathF.Asin(MathF.Sin(2.0f * MathF.PI * y / processor.WaveLength));
            pixelY = 2f * processor.Amplitude / MathF.PI * MathF.Acos(MathF.Cos(2.0f * MathF.PI * x / processor.WaveLength));
        }

        private void SquareWave(in int x, in int y,
            ref float pixelX, ref float pixelY)
        {
            pixelX = MathF.Sign(MathF.Sin(2.0f * MathF.PI * y / processor.WaveLength)) * processor.Amplitude;
            pixelY = MathF.Sign(MathF.Cos(2.0f * MathF.PI * x / processor.WaveLength)) * processor.Amplitude;
        }
    }
}

