using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace ExNihilo.Processors;

internal class RGBShiftProcessor : IImageProcessor
{
    public int BlueYOffset { get; set; }
    public int GreenYOffset { get; set; }
    public int RedYOffset { get; set; }
    public int BlueXOffset { get; set; }
    public int GreenXOffset { get; set; }
    public int RedXOffset { get; set; }

    public Rectangle Area { get; set; }

    public RGBShiftProcessor(int offset)
    {
        RedXOffset = RedYOffset = offset;
        GreenXOffset = GreenYOffset = -offset;
        BlueXOffset = offset;
        BlueYOffset = -offset;
    }


    public RGBShiftProcessor(int blueXOffset, int blueYOffset, int greenXOffset, int greenYOffset, int redXOffset, int redYOffset)
    {
        BlueYOffset = blueYOffset;
        GreenYOffset = greenYOffset;
        RedYOffset = redYOffset;
        BlueXOffset = blueXOffset;
        GreenXOffset = greenXOffset;
        RedXOffset = redXOffset;
    }

    // This is called when we want to build a pixel specific image processor, this is where you get access to the target image to the first time.
    public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        if (Area.Width <= 0 || Area.Height <= 0)
            Area = new Rectangle(0, 0, source.Width, source.Height);

        return new RGBShiftProcessorInner<TPixel>(this, source);
    }

    // the main work horse class this has access to the pixel buffer but in an abstract/generic way.
    private class RGBShiftProcessorInner<TPixel> : IImageProcessor<TPixel>
        where TPixel : unmanaged, IPixel<TPixel>
    {
        private RGBShiftProcessor rgbShiftProcessor;
        private Image<TPixel> source;

        public RGBShiftProcessorInner(RGBShiftProcessor rgbShiftProcessor, Image<TPixel> source)
        {
            this.rgbShiftProcessor = rgbShiftProcessor;
            this.source = source;
        }

        public void Dispose()
        {
        }

        public void Execute()
        {
            var workArea = rgbShiftProcessor.Area;

            // create copy of image
            TPixel[] pixelArray = new TPixel[source.Width * source.Height];
            source.CopyPixelDataTo(pixelArray);

            source.DangerousTryGetSinglePixelMemory(out var pixelsMemory);


            // init vars
            var resPixelArray = pixelsMemory.Span;
            int imageWidth = source.Width, imageHeight = source.Height;

            int width = Math.Min(workArea.Width, source.Width), height = Math.Min(workArea.Height, source.Height);
            
            if (workArea.X + width > source.Width)
                width = source.Width - workArea.X;
            
            if (workArea.Y + height > source.Height)
                height = source.Height - workArea.Y;
            
            Rgba32
                r = new(),
                g = new(),
                b = new();

            for (int y = workArea.Y; y < height + workArea.Y; y++)
            {
                for (int x = workArea.X; x < width + workArea.X; x++)
                {
                    // red shift
                    RGBShiftMethod(
                        pixelArray,
                        in x,
                        in y,
                        rgbShiftProcessor.RedXOffset,
                        rgbShiftProcessor.RedYOffset,
                        in imageWidth,
                        in imageHeight,
                        ref r
                    );
                    
                    // green shift
                    RGBShiftMethod(
                        pixelArray,
                        in x,
                        in y,
                        rgbShiftProcessor.GreenXOffset,
                        rgbShiftProcessor.GreenYOffset,
                        in imageWidth,
                        in imageHeight,
                        ref g
                    );
                    
                    // blue shift
                    RGBShiftMethod(
                        pixelArray,
                        in x,
                        in y,
                        rgbShiftProcessor.BlueXOffset,
                        rgbShiftProcessor.BlueYOffset,
                        in imageWidth,
                        in imageHeight,
                        ref b
                    );

                    var resPixel = new TPixel();
                    resPixel.FromRgba32(new Rgba32(r.R, g.G, b.B, (byte) (r.A | g.A | b.A)));
                    resPixelArray[y * imageWidth + x] = resPixel;
                }
            }
        }

        static void RGBShiftMethod(
            TPixel[] src,
            in int x,
            in int y,
            in int xOffset,
            in int yOffset,
            in int width,
            in int height,
            ref Rgba32 resColor
        ) {
            int
                curXOffset = x + xOffset,
                curYOffset = y + yOffset;

            // check bounds
            if (curXOffset >= width)
                curXOffset -= width;
            else if (curXOffset < 0)
                curXOffset += width;
            
            if (curYOffset >= height)
                curYOffset -= height;
            else if (curYOffset < 0)
                curYOffset += height;

            TPixel sourceColor = src[curYOffset * width + curXOffset];
            sourceColor.ToRgba32(ref resColor);
        }
    }
}

