using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using System.Security.Cryptography;
using Text = ExNihilo.Visuals.Text;

namespace ExNihilo.Tests;

[TestClass]
public class EffectTest : Test
{
    private static readonly Point Center = new(256, 128);

    private static Container CreateTemplate()
    {
        Size containerSize = new(512, 256);
        Point center = new(256, 128);

        int size = 10;
        bool[,] template = new bool[size, size];
        Enumerable.Range(0, template.GetLength(0)).ToList().ForEach(index =>
        {
            template[index, 0] = true;
            template[0, index] = true;
        });

        var fontFamily = new FontCollection().Add(Path.GetFullPath(@"./Assets/Fonts/OpenSans.ttf"));

        return new Container(containerSize)
            .WithBackground(Color.White)
            .WithChild(
                new Pattern()
                    .WithArea(new SixLabors.ImageSharp.Rectangle(0, 0, 512, 256))
                    .WithTemplate(template)
                    .WithForegroundColor(Color.Black)
                    .WithBackgroundColor(Color.Transparent))
            .WithChild(
                new Ellipse()
                    .WithPoint(new Point(256, 128))
                    .WithSize(new Size(100))
                    .WithBrush(brush =>
                    {
                        brush.WithType(BrushType.Horizontal);
                        brush.WithColor(Color.Green);
                    })
                    .WithType(VisualType.Filled))
            .WithChild(
                new Line()
                    .WithPoints(new[] { new PointF(0, 0), new PointF(512, 256) })
                    .WithPen(pen =>
                    {
                        pen.WithColor(Color.Blue);
                        pen.WithWidth(3);
                        pen.WithType(PenType.Dash);
                    }))
            .WithChild(new Picture(@"./Assets/Images/cat.png"))
            .WithChild(
                new Polygon()
                    .WithPoints(new[] { new PointF(400, 200), new PointF(350, 150), new PointF(300, 179) })
                    .WithBrush(BrushType.Solid, Color.Peru)
                    .WithType(VisualType.Filled))
            .WithChild(
                new Rectangle(50, 200, 100, 50)
                    .WithPen(PenType.DashDot, 5, Color.Olive)
                    .WithType(VisualType.Outlined))
            .WithChild(
                new Text(fontFamily, "HELLO")
                    .WithPoint(center)
                    .WithFontSize(72)
                    .WithBrush(Color.Green)
                    .WithType(VisualType.Filled));
    }

    private static Image<Rgba32> RenderBase()
    {
        var template = CreateTemplate();
        template.Randomize(new Random(1));
        return template.Render().CloneAs<Rgba32>();
    }

    private static Image<Rgba32> Render(Effect effect)
    {
        var template = CreateTemplate().WithEffect(effect);
        template.Randomize(new Random(1));
        return template.Render().CloneAs<Rgba32>();
    }

    private static void AssertValidImage(Image<Rgba32> image)
    {
        Assert.IsNotNull(image);
        Assert.IsTrue(image.Width > 0, "Image width should be greater than zero.");
        Assert.IsTrue(image.Height > 0, "Image height should be greater than zero.");
    }

    private static string ComputePixelHash(Image<Rgba32> image)
    {
        using var sha256 = SHA256.Create();
        using var ms = new MemoryStream();

        static void WriteInt(Stream stream, int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            stream.Write(bytes, 0, bytes.Length);
        }

        WriteInt(ms, image.Width);
        WriteInt(ms, image.Height);

        for (int y = 0; y < image.Height; y++)
        {
            Span<Rgba32> row = image.DangerousGetPixelRowMemory(y).Span;
            for (int x = 0; x < row.Length; x++)
            {
                ms.WriteByte(row[x].R);
                ms.WriteByte(row[x].G);
                ms.WriteByte(row[x].B);
                ms.WriteByte(row[x].A);
            }
        }

        ms.Position = 0;
        byte[] hash = sha256.ComputeHash(ms);
        return Convert.ToHexString(hash);
    }

    private static bool ImagesAreEqual(Image<Rgba32> left, Image<Rgba32> right)
    {
        if (left.Width != right.Width || left.Height != right.Height)
            return false;

        for (int y = 0; y < left.Height; y++)
        {
            Span<Rgba32> leftRow = left.DangerousGetPixelRowMemory(y).Span;
            Span<Rgba32> rightRow = right.DangerousGetPixelRowMemory(y).Span;

            for (int x = 0; x < leftRow.Length; x++)
            {
                if (!leftRow[x].Equals(rightRow[x]))
                    return false;
            }
        }

        return true;
    }

    private static int CountDifferentPixels(Image<Rgba32> left, Image<Rgba32> right)
    {
        Assert.AreEqual(left.Width, right.Width, "Images must have the same width.");
        Assert.AreEqual(left.Height, right.Height, "Images must have the same height.");

        int count = 0;

        for (int y = 0; y < left.Height; y++)
        {
            Span<Rgba32> leftRow = left.DangerousGetPixelRowMemory(y).Span;
            Span<Rgba32> rightRow = right.DangerousGetPixelRowMemory(y).Span;

            for (int x = 0; x < leftRow.Length; x++)
            {
                if (!leftRow[x].Equals(rightRow[x]))
                    count++;
            }
        }

        return count;
    }

    private static void AssertRenderSucceeds(IEnumerable<Effect> effects)
    {
        foreach (Effect effect in effects)
        {
            using Image<Rgba32> image = Render(effect);
            AssertValidImage(image);
        }
    }

    [TestMethod]
    public void Smoke_ColorEffects_ShouldRender()
    {
        List<Effect> effects = new()
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
                    0, 0, 0, 0)),
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

        AssertRenderSucceeds(effects);
    }

    [TestMethod]
    public void Smoke_ConvolutionalEffects_ShouldRender()
    {
        List<Effect> effects = new()
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

        AssertRenderSucceeds(effects);
    }

    [TestMethod]
    public void Smoke_DistortEffects_ShouldRender()
    {
        List<Effect> effects = new()
        {
            new Bulge(Center, 100, 0.5f),
            new RgbShift(3),
            new Ripple(Center, 250, 10, 3),
            new Slices(15, 6),
            new SlitScan(0.5f),
            new Swirl(Center, 150, 10, 1.5f),
            new Wave(100, 10, Processors.WaveType.Sine)
        };

        AssertRenderSucceeds(effects);
    }

    [TestMethod]
    public void Smoke_NoiseEffects_ShouldRender()
    {
        List<Effect> effects = new()
        {
            new GaussianNoise(0.5f, false),
            new PerlinNoise(3, 0.5f, 0.5f, false)
        };

        AssertRenderSucceeds(effects);
    }

    [TestMethod]
    public void Smoke_TransformEffects_ShouldRender()
    {
        List<Effect> effects = new()
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

        AssertRenderSucceeds(effects);
    }

    [TestMethod]
    public void GrayScale_ShouldMakeRgbChannelsEqual()
    {
        using Image<Rgba32> image = Render(new GrayScale());

        int checkedPixels = ForEachOpaquePixel(image, 1000, pixel =>
        {
            Assert.AreEqual(pixel.R, pixel.G, "Grayscale pixel should have R == G.");
            Assert.AreEqual(pixel.G, pixel.B, "Grayscale pixel should have G == B.");
        });

        Assert.IsTrue(checkedPixels > 0, "No opaque pixels were checked.");
    }

    [TestMethod]
    public void Crop_ShouldReturnExpectedSize()
    {
        using Image<Rgba32> image = Render(new Crop().WithSize(new Size(200, 200)));

        Assert.AreEqual(200, image.Width);
        Assert.AreEqual(200, image.Height);
    }

    [TestMethod]
    public void Resize_ShouldReturnExpectedSize()
    {
        using Image<Rgba32> image = Render(new Resize(256, 100));

        Assert.AreEqual(256, image.Width);
        Assert.AreEqual(100, image.Height);
    }

    [TestMethod]
    public void Pad_ShouldReturnExpectedSize()
    {
        using Image<Rgba32> image = Render(new Pad(256, 100));

        Assert.AreEqual(512, image.Width);
        Assert.AreEqual(256, image.Height);
    }

    [TestMethod]
    public void FlipVertical_ShouldChangePixels()
    {
        using Image<Rgba32> original = RenderBase();
        using Image<Rgba32> flipped = Render(new Flip(SixLabors.ImageSharp.Processing.FlipMode.Vertical));

        Assert.IsFalse(ImagesAreEqual(original, flipped), "Vertical flip should change image pixels.");
    }

    [TestMethod]
    public void Invert_ShouldChangePixels()
    {
        using Image<Rgba32> original = RenderBase();
        using Image<Rgba32> inverted = Render(new Invert());

        int differentPixels = CountDifferentPixels(original, inverted);
        Assert.IsTrue(differentPixels > 0, "Invert should change at least some pixels.");
    }

    [TestMethod]
    public void Opacity_Half_ShouldReduceAlphaForSomePixels()
    {
        using Image<Rgba32> original = RenderBase();
        using Image<Rgba32> changed = Render(new Opacity(0.5f));

        bool foundReducedAlpha = false;

        for (int y = 0; y < original.Height && !foundReducedAlpha; y++)
        {
            Span<Rgba32> originalRow = original.DangerousGetPixelRowMemory(y).Span;
            Span<Rgba32> changedRow = changed.DangerousGetPixelRowMemory(y).Span;

            for (int x = 0; x < originalRow.Length; x++)
            {
                if (changedRow[x].A < originalRow[x].A)
                {
                    foundReducedAlpha = true;
                    break;
                }
            }
            if (foundReducedAlpha)
                break;
        }

        Assert.IsTrue(foundReducedAlpha, "Opacity(0.5f) should reduce alpha for at least some pixels.");
    }

    [TestMethod]
    [DataRow("GrayScale", "03CB6A35A81C44AB46F240F8FE740A6353545417889E93AE0DB948274FC9A166")]
    [DataRow("Invert", "181CBF812EBAD999E345D3527B7C317900577CF8EFB66D3A77B0ACA59EBF485B")]
    [DataRow("Sepia", "35B7D48B587662F9CB0EABC59A4FC93CC19BEE52B02730BEEBC4BE3A99949723")]
    [DataRow("FlipVertical", "FFFDB4E287B01893F6E593DCF0A17927D7EF43E161D7B98370714CE37CD185D1")]
    public void Snapshot_DeterministicEffects_ShouldMatchExpectedHash(string effectName, string expectedHash)
    {
        Effect effect = effectName switch
        {
            "GrayScale" => new GrayScale(),
            "Invert" => new Invert(),
            "Sepia" => new Sepia(),
            "FlipVertical" => new Flip(SixLabors.ImageSharp.Processing.FlipMode.Vertical),
            _ => throw new ArgumentOutOfRangeException(nameof(effectName), effectName, null)
        };

        using Image<Rgba32> image = Render(effect);
        string actualHash = ComputePixelHash(image);
        
        Assert.AreEqual(
            expectedHash,
            actualHash,
            $"Snapshot mismatch for '{effectName}'. New hash: {actualHash}");
    }

    private static int ForEachOpaquePixel(Image<Rgba32> image, int maxCount, Action<Rgba32> action)
    {
        int processed = 0;

        for (int y = 0; y < image.Height; y++)
        {
            Span<Rgba32> row = image.DangerousGetPixelRowMemory(y).Span;

            for (int x = 0; x < row.Length; x++)
            {
                Rgba32 pixel = row[x];
                if (pixel.A == 0)
                    continue;

                action(pixel);

                processed++;
                if (processed >= maxCount)
                    return processed;
            }
        }

        return processed;
    }
}