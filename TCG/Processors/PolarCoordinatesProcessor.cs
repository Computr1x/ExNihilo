using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace TCG.Processors;

public class PolarCoordinatesProcessor : IImageProcessor
{
    public PolarConversionType PolarType { get; set; } = PolarConversionType.CartesianToPolar;

    public Rectangle Area { get; set; }


    public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        if (Area.Width <= 0 || Area.Height <= 0)
            Area = new Rectangle(0, 0, source.Width, source.Height);

        return new PolarCoordinatesProcessorInner<TPixel>(this, source);
    }

    // the main work horse class this has access to the pixel buffer but in an abstract/generic way.
    private class PolarCoordinatesProcessorInner<TPixel> : IImageProcessor<TPixel>
        where TPixel : unmanaged, IPixel<TPixel>
    {
        private PolarCoordinatesProcessor processor;
        private Image<TPixel> source;

        private delegate void CalcPolarDelegate(ref int x, ref int y, ref float scaleX, ref float scaleY, ref float centerX, ref float centerY,
                ref float pixelX, ref float pixelY, ref float offsetX, ref float offsetY);

        public PolarCoordinatesProcessorInner(PolarCoordinatesProcessor processor, Image<TPixel> source)
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
            // calculate work area
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
            float centerX = workArea.X + width / 2f, centerY = workArea.Y + height / 2f,
            pixelX = 0, pixelY = 0, offsetX = 0, offsetY = 0,
            maxRadius = MathF.Sqrt(width * width + height * height) / 4f,
            scaleX = width / maxRadius, scaleY = height / (2f * MathF.PI);
            int intOffsetX = 0, intOffsetY = 0;
            Rgba32 sourcePixel = new();


            CalcPolarDelegate CalcPolar = processor.PolarType == PolarConversionType.CartesianToPolar ? RectangularToPolar : PolarToCartesian;


            source.ProcessPixelRows(accessor =>
            {
                for (int y = workArea.Y; y < height + workArea.Y; y++)
                {
                    Span<TPixel> pixelRow = accessor.GetRowSpan(y);

                    for (int x = workArea.X; x < width + workArea.X; x++)
                    {
                        CalcPolar(ref x, ref y, ref scaleX, ref scaleY, ref centerX, ref centerY,
                            ref pixelX, ref pixelY, ref offsetX, ref offsetY);

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

        private void RectangularToPolar(ref int x, ref int y, ref float scaleX, ref float scaleY, ref float centerX, ref float centerY,
                ref float pixelX, ref float pixelY, ref float offsetX, ref float offsetY)
        {
            pixelX = x - centerX;
            pixelY = y - centerY;

            // pixelDistance = MathF.Sqrt(pixelX * pixelX + pixelY * pixelY)
            // pixelAngle = MathF.Atan2(pixelY, pixelX)

            offsetX = MathF.Sqrt(pixelX * pixelX + pixelY * pixelY) * scaleX;
            offsetY = (MathF.Atan2(pixelY, pixelX) + MathF.PI) /*% (2f * MathF.PI)*/ * scaleY;
        }

        private void PolarToCartesian(ref int x, ref int y, ref float scaleX, ref float scaleY, ref float centerX, ref float centerY,
            ref float pixelX, ref float pixelY, ref float offsetX, ref float offsetY)
        {
            pixelX = x / scaleX;
            pixelY = y / scaleY - MathF.PI;

            offsetX = pixelX * MathF.Cos(pixelY) + centerX;
            offsetY = pixelX * MathF.Sin(pixelY) + centerY;
        }
    }
}

public enum PolarConversionType { CartesianToPolar, PolarToCartesian };