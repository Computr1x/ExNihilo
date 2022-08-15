using ExNihilo.Extensions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace ExNihilo.Processors;

internal class HSBCorrectionProcessor : IImageProcessor
{
    private int hue, saturation, brightness;

    public int Hue
    {
        get => hue;
        set { hue = (sbyte) (value % 256); }
    }

    public int Saturation
    {
        get => saturation;
        set { saturation = (sbyte) (value % 256); }
    }

    public int Brightness
    {
        get => brightness;
        set { brightness = (sbyte) (value % 256); }
    }

    public Rectangle Area { get; set; }


    public HSBCorrectionProcessor()
    {
    }

    public HSBCorrectionProcessor(int hue, int saturation, int brightness)
    {
        Hue = hue;
        Saturation = saturation;
        Brightness = brightness;
    }


    // This is called when we want to build a pixel specific image processor, this is where you get access to the target image to the first time.
    public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        if (Area.Width <= 0 || Area.Height <= 0)
            Area = new Rectangle(0, 0, source.Width, source.Height);

        return new HSBCorrectionProcessorInner<TPixel>(this, source);
    }

    // the main work horse class this has access to the pixel buffer but in an abstract/generic way.
    private class HSBCorrectionProcessorInner<TPixel> : IImageProcessor<TPixel>
        where TPixel : unmanaged, IPixel<TPixel>
    {
        private readonly HSBCorrectionProcessor processor;
        private readonly Image<TPixel> source;

        public HSBCorrectionProcessorInner(HSBCorrectionProcessor processor, Image<TPixel> source)
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

            // init vars
            int
                width = Math.Min(workArea.Width, source.Width),
                height = Math.Min(workArea.Height, source.Height),

                imageWidth = source.Width,
                imageHeight = source.Height;
            
            if (workArea.X + width > source.Width)
                width = source.Width - workArea.X;
            
            if (workArea.Y + height > source.Height)
                height = source.Height - workArea.Y;

            Rgba32 sourcePixel = new();
            TPixel rawPixel = new();
            
            byte h, s, v;

            // addititonal fields for ref optimization
            byte
                region = 0,
                remainder = 0,
                p = 0,
                q = 0,
                t = 0;

            source.ProcessPixelRows(accessor =>
            {
                for (int y = workArea.Y; y < height + workArea.Y; y++)
                {
                    Span<TPixel> pixelRow = accessor.GetRowSpan(y);

                    for (int x = workArea.X; x < width + workArea.X; x++)
                    {
                        pixelRow[x].ToRgba32(ref sourcePixel);

                        ColorsConverter.RgbToHsb(in sourcePixel.R, in sourcePixel.G, in sourcePixel.B, out h, out s, out v);

                        h = (byte) Math.Clamp(h + processor.Hue, 0, 255);
                        s = (byte) Math.Clamp(s + processor.Saturation, 0, 255);
                        v = (byte) Math.Clamp(v + processor.Brightness, 0, 255);

                        ColorsConverter.HsbToRgb(in h, in s, in v, ref region, ref remainder, ref p, ref q, ref t, out sourcePixel.R, out sourcePixel.G, out sourcePixel.B);

                        var resPixel = new TPixel();
                        resPixel.FromRgba32(sourcePixel);
                        pixelRow[x] = resPixel;
                    }
                }
            });
        }
    }
}

