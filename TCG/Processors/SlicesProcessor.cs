using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;

namespace TCG.Processors;

public class SlicesProcessor : IImageProcessor
{
    public Rectangle Area { get; set; }

    private Random _r;
    public int Count { get; set; } = 1;
    public int MinOffset { get; set; } = -10;
    public int MaxOffset { get; set; } = 20;

    public int SliceHeight { get; set; } = 4;

    public SlicesProcessor(int seed)
    {
        _r = new Random(seed);
    }

    public IImageProcessor<TPixel> CreatePixelSpecificProcessor<TPixel>(Configuration configuration, Image<TPixel> source, Rectangle sourceRectangle)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        if (Area.Width <= 0 || Area.Height <= 0)
            Area = new Rectangle(0, 0, source.Width, source.Height);

        return new SlicesProcessorInner<TPixel>(this, source);
    }

    private class SlicesProcessorInner<TPixel> : IImageProcessor<TPixel>
        where TPixel : unmanaged, IPixel<TPixel>
    {
        private SlicesProcessor processor;
        private Image<TPixel> source;

        public SlicesProcessorInner(SlicesProcessor processor, Image<TPixel> source)
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
            float offsetX = 0, offsetY = 0;
            int intOffsetX = 0, intOffsetY = 0;
            Rgba32 sourcePixel = new();

            int[] sliceIndexes = Enumerable.Range(0, processor.Count).Select(x => processor._r.Next(workArea.Y, height + workArea.Y)).OrderBy(x => x).ToArray();
            int[] sliceOffsets = Enumerable.Range(0, processor.Count).Select(x => processor._r.Next(0, processor.MaxOffset)).ToArray();


            source.ProcessPixelRows(accessor =>
            {
                for (int i = 0; i < processor.Count; i++)
                {
                    for (int y = sliceIndexes[i]; y < sliceIndexes[i] + processor.SliceHeight && y < height + workArea.Y; y++)
                    {
                        Span<TPixel> pixelRow = accessor.GetRowSpan(y);

                        for (int x = workArea.X, offsetX = sliceOffsets[i]; x < workArea.X + width; x++, offsetX++)
                        {
                            if (offsetX >= width + workArea.X)
                                offsetX = workArea.X;
                            else if (offsetX < workArea.X)
                                offsetX = width + workArea.X + sliceOffsets[i];

                            intOffsetX = (int)offsetX;
                            intOffsetY = (int)offsetY;

                            TPixel resPixel = imageCopyArray[intOffsetY * source.Width + intOffsetX];
                            resPixel.ToRgba32(ref sourcePixel);
                            pixelRow[x].FromRgba32(sourcePixel);
                        }
                    }
                }
            });
        }
    }
}

