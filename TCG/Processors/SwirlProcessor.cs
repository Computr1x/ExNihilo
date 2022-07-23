using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace TCG.Processors;

public class SwirlProcessor : IImageProcessor
{
    private bool customCoords = false;

    public Rectangle Area { get; set; }

    public float Degree { get; set; } = 0;
    public float Radius { get; set; }
    public float Twists { get; set; }
    public bool Cloakwise { get; set; } = true;

    public int X { get; }
    public int Y { get; }

    public SwirlProcessor(float radius)
    {
        Radius = radius;
    }

    public SwirlProcessor(float radius, float degree, float twists) : this(radius)
    {
        Twists = twists;
        Degree = degree;
    }

    public SwirlProcessor(int x, int y, float radius, float degree, float twists) : this(radius, degree, twists)
    {
        X = x;
        Y = y;
        customCoords = true;
    }

    public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        if (Area.Width <= 0 || Area.Height <= 0)
            Area = new Rectangle(0, 0, source.Width, source.Height);

        return new SwirlProcessorInner<TPixel>(this, source);
    }

    private class SwirlProcessorInner<TPixel> : IImageProcessor<TPixel>
        where TPixel : unmanaged, IPixel<TPixel>
    {
        private SwirlProcessor processor;
        private Image<TPixel> source;

        public SwirlProcessorInner(SwirlProcessor processor, Image<TPixel> source)
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
            float swirlX = width / 2.0f, swirlY = height / 2.0f,
                pixelX = 0, pixelY = 0, pixelDistance = 0, pixelAngle = 0, twistAngle = 0, swirlAmount = 0,
                offsetX = 0, offsetY = 0;
            int intOffsetX = 0, intOffsetY = 0;
            Rgba32 sourcePixel = new();

            if (processor.customCoords)
            {
                swirlX = processor.X;
                swirlY = processor.Y;
            }
            if (processor.Cloakwise)
                processor.Twists = Math.Abs(processor.Twists);
            else
                processor.Twists = -Math.Abs(processor.Twists);


            source.ProcessPixelRows(accessor =>
            {
                for (int y = workArea.Y; y < height + workArea.Y; y++)
                {
                    Span<TPixel> pixelRow = accessor.GetRowSpan(y);

                    for (int x = workArea.X; x < width + workArea.X; x++)
                    {
                        CalculateSwirl(in x, in y, ref pixelX, ref pixelY, ref pixelDistance,
                            ref pixelAngle, ref twistAngle, ref swirlAmount,
                            ref swirlX, ref swirlY, ref offsetX, ref offsetY);

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

        private void CalculateSwirl(in int x, in int y, ref float pixelX, ref float pixelY, ref float pixelDistance,
                 ref float pixelAngle, ref float twistAngle, ref float swirlAmount,
                 ref float swirlX, ref float swirlY, ref float offsetX, ref float offsetY)
        {
            pixelX = x - swirlX;
            pixelY = y - swirlY;

            // compute the distance and angle from the swirl center:
            pixelDistance = MathF.Sqrt(pixelX * pixelX + pixelY * pixelY);
            pixelAngle = MathF.Atan2(pixelY, pixelX);

            // work out how much of a swirl to apply (1.0 in the center fading out to 0.0 at the radius):
            swirlAmount = 1.0f - (pixelDistance / processor.Radius);
            if (swirlAmount > 0.0f)
            {
                twistAngle = processor.Twists * swirlAmount * MathF.PI * 2.0f + processor.Degree;

                // adjust the pixel angle and compute the adjusted pixel co-ordinates:
                pixelAngle += twistAngle + processor.Degree;
                pixelX = MathF.Cos(pixelAngle) * pixelDistance;
                pixelY = MathF.Sin(pixelAngle) * pixelDistance;
            }
            offsetX = swirlX + pixelX;
            offsetY = swirlY + pixelY;
        }
    }
}

