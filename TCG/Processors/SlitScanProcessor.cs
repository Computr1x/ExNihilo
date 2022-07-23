using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace TCG.Processors;

public class SlitScanProcessor : IImageProcessor
{
    public float Time { get; }

    public Rectangle Area { get; set; }

    public SlitScanProcessor()
    {
    }

    public SlitScanProcessor(float time)
    {
        Time = time;
    }
    

    public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        if (Area.Width <= 0 || Area.Height <= 0)
            Area = new Rectangle(0, 0, source.Width, source.Height);

        return new SlitScanProcessorInner<TPixel>(this, source);
    }

    private class SlitScanProcessorInner<TPixel> : IImageProcessor<TPixel>
        where TPixel : unmanaged, IPixel<TPixel>
    {
        private SlitScanProcessor processor;
        private Image<TPixel> source;

        public SlitScanProcessorInner(SlitScanProcessor processor, Image<TPixel> source)
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
            float t2 = processor.Time * 0.37f, time = processor.Time;
            float offsetY = 0, v = 0, offset = 0, offset1 = 0, offset2 = 0;
            int intOffsetX = 0, intOffsetY = 0;
            Rgba32 sourcePixel = new();


            source.ProcessPixelRows(accessor =>
            {
                for (int y = workArea.Y; y < height + workArea.Y; y++)
                {
                    Span<TPixel> pixelRow = accessor.GetRowSpan(y);

                    v = y / (float)height;

                    offset1 = MathF.Sin((v + 0.5f) * Mix(3f, 12f, UpDown(time))) * 15;
                    offset2 = MathF.Sin((v + 0.5f) * Mix(3f, 12f, UpDown(t2))) * 15;
                    offset = offset1 + offset2;

                    offsetY = y * height / (float)height + offset;
                    offsetY = Math.Max(0, Math.Min(height - 1, offsetY));

                    for (int x = workArea.X; x < width + workArea.X; x++)
                    {
                        intOffsetX = (int)x;
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

        private float Mix(float a, float b, float l)
        {
            return a + (b - a) * l;
        }

        private float UpDown(float v)
        {
            return MathF.Sin(v) * 0.5f + 0.5f;
        }
    }
}

