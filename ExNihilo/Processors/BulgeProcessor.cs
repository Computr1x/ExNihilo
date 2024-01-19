using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace ExNihilo.Processors;

internal class BulgeProcessor : IImageProcessor
{
    public int X { get; }
    public int Y { get; }
    public float Radius { get; }
    public float Strenght { get; }

    public Rectangle Area { get; set; }

    public BulgeProcessor(int x, int y, float radius, float strenght)
    {
        X = x;
        Y = y;
        Radius = radius;
        Strenght = strenght;
    }

    public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        if (Area.Width <= 0 || Area.Height <= 0)
            Area = new Rectangle(0, 0, source.Width, source.Height);

        return new BulgeProcessorInner<TPixel>(this, source);
    }

    private class BulgeProcessorInner<TPixel> : IImageProcessor<TPixel>
        where TPixel : unmanaged, IPixel<TPixel>
    {
        private readonly BulgeProcessor processor;
        private readonly Image<TPixel> source;

        public BulgeProcessorInner(BulgeProcessor processor, Image<TPixel> source)
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
            float distance = 0, interpolationFactor = 0;
            float pixelX = 0, pixelY = 0, offsetX = 0, offsetY = 0, pixelDistance = 0, pixelAngle = 0;
            int intOffsetX = 0, intOffsetY = 0;
            Rgba32 sourcePixel = new();


            source.ProcessPixelRows(accessor =>
            {
                for (int y = workArea.Y; y < height + workArea.Y; y++)
                {
                    Span<TPixel> pixelRow = accessor.GetRowSpan(y);

                    for (int x = workArea.X; x < width + workArea.X; x++)
                    {
                        distance = MathF.Sqrt(MathF.Pow(x - processor.X, 2f) + MathF.Pow(y - processor.Y, 2f));


                        pixelRow[x].ToRgba32(ref sourcePixel);
                        if (distance <= processor.Radius)
                        {
                            CalculateBulge(in x, in y, ref interpolationFactor, ref pixelDistance, ref pixelAngle,
                                    ref pixelX, ref pixelY, ref offsetX, ref offsetY);

                            intOffsetX = (int) offsetX;
                            intOffsetY = (int) offsetY;

                            if (0 <= intOffsetX && intOffsetX < imageWidth && 0 <= intOffsetY && intOffsetY < imageHeight)
                            {
                                TPixel resPixel = imageCopyArray[intOffsetY * source.Width + intOffsetX];
                                resPixel.ToRgba32(ref sourcePixel);
                                pixelRow[x].FromRgba32(sourcePixel);
                            }
                        }
                    }
                }
            });
        }

        private void CalculateBulge(in int x, in int y,
            ref float interpolationFactor,
            ref float pixelDistance, ref float pixelAngle,
            ref float pixelX, ref float pixelY,
            ref float xOffset, ref float yOffset)
        {
            pixelX = x - processor.X;
            pixelY = y - processor.Y;

            pixelDistance = MathF.Sqrt(pixelX * pixelX + pixelY * pixelY);
            pixelAngle = MathF.Atan2(pixelY, pixelX);

            interpolationFactor = pixelDistance / processor.Radius;
            pixelDistance = interpolationFactor * pixelDistance + (1.0f - interpolationFactor) * processor.Strenght * MathF.Sqrt(pixelDistance);

            xOffset = MathF.Cos(pixelAngle) * pixelDistance + processor.X;
            yOffset = MathF.Sin(pixelAngle) * pixelDistance + processor.Y;
        }
    }
}

