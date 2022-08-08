using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace TCG.Processors;

internal class CrystallizeProcessor : IImageProcessor
{
    public Rectangle Area { get; set; }

    private readonly Random _r;
    public int CrystalsCount { get; set; } = 64;


    public CrystallizeProcessor(int seed = 0)
    {
        _r = new Random(seed);
    }

    public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        if (Area.Width <= 0 || Area.Height <= 0)
            Area = new Rectangle(0, 0, source.Width, source.Height);

        return new CrystallizeProcessorInner<TPixel>(this, source);
    }

    private class CrystallizeProcessorInner<TPixel> : IImageProcessor<TPixel>
        where TPixel : unmanaged, IPixel<TPixel>
    {
        private CrystallizeProcessor processor;
        private Image<TPixel> source;

        public CrystallizeProcessorInner(CrystallizeProcessor processor, Image<TPixel> source)
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
            // centers of crystals
            uint[] xn = Enumerable.Range(0, processor.CrystalsCount).Select(x => (uint)processor._r.Next(workArea.X, width + workArea.X)).ToArray();
            uint[] yn = Enumerable.Range(0, processor.CrystalsCount).Select(x => (uint)processor._r.Next(workArea.Y, height + workArea.Y)).ToArray();
            uint d = 0, dMin = 0, dIndex = 0; ;
            Rgba32 sourcePixel = new();


            source.ProcessPixelRows(accessor =>
            {
                for (int y = workArea.Y; y < height + workArea.Y; y++)
                {
                    Span<TPixel> pixelRow = accessor.GetRowSpan(y);

                    for (int x = workArea.X; x < width + workArea.X; x++)
                    {
                        // find neares crystal center
                        dMin = uint.MaxValue;
                        for (uint i = 0; i < processor.CrystalsCount; i++)
                        {
                            // calculate distance
                            d = (uint)((y - yn[i]) * (y - yn[i]) + (x - xn[i]) * (x - xn[i]));

                            if (d >= dMin) continue;
                            dMin = d;
                            dIndex = i;
                        }

                        TPixel resPixel = imageCopyArray[yn[dIndex] * width + xn[dIndex]];
                        resPixel.ToRgba32(ref sourcePixel);
                        pixelRow[x].FromRgba32(sourcePixel);
                    }
                }
            });
        }
    }
}

