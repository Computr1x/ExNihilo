using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using System.Globalization;

namespace ExNihilo.Tests;

[TestClass]
public class EffectTest : Test
{
    private static int samplesCount = 1;
    private static Point center = new(256, 128);

    private static Container CreateTemplate()
    {
        Size containerSize = new(512, 256);
        Point center = new(256, 128);

        int size = 10;
        bool[,] template = new bool[size, size];
        Enumerable.Range(0, template.GetLength(0)).ToList().ForEach((index) =>
        {
            template[index, 0] = true;
            template[0, index] = true;
        });
        
        var fontFamily = new FontCollection().Add(".\\Assets\\Fonts\\OpenSans.ttf");
        return new Container(containerSize)
            .WithBackground(Color.White)
            .WithChild(
                new Pattern()
                    .WithArea(new SixLabors.ImageSharp.Rectangle(0, 0, 512, 256))
                    .WithTemplate(template)
                    .WithForegroundColor(Color.Black)
                    .WithBackgroundColor(Color.Transparent)
            )
            .WithChild(
                new Ellipse()
                .WithPoint(new Point(256, 128))
                .WithSize(new Size(100))
                .WithBrush(brush =>
                {
                    brush.WithType(BrushType.Horizontal);
                    brush.WithColor(Color.Green);
                })
                .WithType(VisualType.Filled)
            )
            .WithChild(
                new Line()
                .WithPoints(new PointF[] { new PointF(0, 0), new PointF(512, 256) })
                .WithPen(pen =>
                {
                    pen.WithColor(Color.Blue);
                    pen.WithWidth(3);
                    pen.WithType(PenType.Dash);
                })
            )
            .WithChild(new Picture(@"./Assets/Images/cat.png"))
            .WithChild(
                new Polygon()
                .WithPoints(new PointF[] { new(400, 200), new(350, 150), new(300, 179) })
                .WithBrush(BrushType.Solid , Color.Peru)
                .WithType(VisualType.Filled)
            )
            .WithChild(
                new Rectangle(50, 200, 100, 50)
                    .WithPen(PenType.DashDot, 5, Color.Olive)
                    .WithType(VisualType.Outlined)
            )
            .WithChild(
                new Text(fontFamily, "HELLO")
                    .WithPoint(center)
                    .WithFontSize(72)
                    .WithBrush(Color.Green)
                    .WithType(VisualType.Filled));
    }

    private void GenerateTemplateWithEffect(Effect effect)
    {
        Container template = CreateTemplate().WithEffect(effect);
        new ImageSaver(
            new ImageGenerator(template).WithSeedsCount(1).Generate())
            .WithOutputPath(CurrentPath).WithFilePrefix(effect.GetType().Name + '_').Save();
    }

    [TestMethod]
    public void TestColorEffects()
    {
        List<Effect> effects = new List<Effect>()
        {
            new AdaptiveThreshold().WithThresholdLimit(0.5f),
            new BinaryThreshold().WithThresholdLimit(0.5f),
            new BlackWhite(),
            new ColorBlindness(),
            new Contrast(1.5f),
            new Dithering(),
            new FilterMatrix(
                new ColorMatrix(
                    2, 0, 0, 0,
                    0, 1, 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1,
                    0, 0, 0, 0
                )),
            new Glow(100),
            new GrayScale(),
            new HistogramEqualization(),
            new HSBCorrection(25, 49, -59),
            new Invert(),
            new KodaChrome(),
            new Lightness(0.5f),
            new Lomograph(),
            new Opacity(0.5f),
            new Polaroid(),
            new Sepia(),
            new Vignette()
        };
        foreach(var effect in effects)
            GenerateTemplateWithEffect(effect);
    }

    [TestMethod]
    public void TestConvultional()
    {
        List<Effect> effects = new List<Effect>()
        {
            new BokehBlur(),
            new BoxBlur(),
            new Crystallize(),
            new EdgeDetection(),
            new GaussianBlur(),
            new GaussianSharpen(),
            new OilPaint(),
            new Pixelate(6),
            new Quantize()
        };
        foreach (var effect in effects)
            GenerateTemplateWithEffect(effect);
    }

    [TestMethod]
    public void TestDistort()
    {
        List<Effect> effects = new List<Effect>()
        {
            new Bulge(center, 100, 0.5f),
            new RgbShift(3),
            new Ripple(center, 250, 10, 3),
            new Slices(10, 3),
            new SlitScan(0.5f),
            new Swirl(center, 150, 10, 1.5f),
            new Wave(100, 10, Processors.WaveType.Sine)
        };
        foreach (var effect in effects)
            GenerateTemplateWithEffect(effect);
    }

    [TestMethod]
    public void TestNoise()
    {
        List<Effect> effects = new List<Effect>()
        {
            new GaussianNoise(),
            new PerlinNoise(3, 0.5f, false)
        };
        foreach (var effect in effects)
            GenerateTemplateWithEffect(effect);
    }

    [TestMethod]
    public void TestTransform()
    {
        List<Effect> effects = new List<Effect>()
        {
            new Crop().WithSize(new Size(200, 200)),
            new EntropyCrop(0.5f),
            new Flip(SixLabors.ImageSharp.Processing.FlipMode.Vertical),
            new Pad(256, 100),
            new PolarCoordinates(Processors.PolarConversionType.CartesianToPolar),
            new Resize(256, 100),
            new Rotate(30),
            new Scale(1.5f, 1.5f),
            new Shift(100, 0),
            new Skew(30, 0)
        };
        foreach (var effect in effects)
            GenerateTemplateWithEffect(effect);
    }

    //[TestMethod]
    //public void TestSwirl()
    //{
    //    GenerateTemplateWithEffect(new Swirl(center, 75, 10, 0.15f));
    //}
}
