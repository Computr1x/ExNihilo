using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using TCG.Base.Hierarchy;
using TCG.Base.Utils;
using TCG.Drawables;
using TCG.Rnd;

namespace TCG.Tests
{
    [TestClass]
    public class GeneratorTest
    {
        static string currentPath;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            currentPath = Path.Combine(Directory.GetCurrentDirectory(), "GeneratorTest");
            if(Directory.Exists(currentPath))
                Directory.Delete(currentPath, true);
            Directory.CreateDirectory(currentPath);
        }

        private Canvas CreateTemplate()
        {
            Size canvasSize = new Size(512, 256);
            Point center = new Point(256, 128);

            var fontFamily = new FontCollection().AddSystemFonts().Families.First();

            return new Canvas(canvasSize).WithLayer(new Layer(canvasSize).WithBackground(Color.Orange)).
                WithLayer(
                    new Layer(canvasSize)
                        .WithDrawables(
                            Enumerable.Range(0, 15).Select(
                                x => new Ellipse()
                                    .WithRandomizedPoint(0, 512, 0, 256)
                                    .WithRandomizedSize(30, 60)
                                    .WithBrush(brush =>
                                    {
                                        brush.WithRandomizedColor(50);
                                        brush.WithRandomizedType();
                                    })
                                    .WithType(DrawableType.Filled)))
                        .WithBlendPercentage(0.5f))
                .WithLayer(
                    new Layer(canvasSize)
                        .WithDrawable(
                            new Captcha(fontFamily)
                                .WithRandomizedContent(content =>
                                {
                                    content.WithLength(6);
                                })
                                .WithPoint(center)
                                .WithFontSize(100)
                                .WithRandomizedBrush(50)
                                .WithType(DrawableType.Filled)));
        }

        [TestMethod]
        public void TestGeneratorWithSeeds()
        {
            foreach(var captchaRes in new CaptchaGenerator(CreateTemplate()).WithSeedsCount(5).Generate())
            {
                captchaRes.Image.SaveAsPng(Path.Combine(currentPath, captchaRes.GetName() + ".png"));
            }
        }

        [TestMethod]
        public void TestGeneratorWithText()
        {
            foreach (var captchaRes in new CaptchaGenerator(CreateTemplate()).WithCaptchaInput(new string[] { "Igor", "so", "small"}).Generate())
            {
                captchaRes.Image.SaveAsPng(Path.Combine(currentPath, captchaRes.GetName() + ".png"));
            }
        }

        [TestMethod]
        public void TestGeneratorWithoutParameters()
        {
            try
            {
                foreach (var captchaRes in new CaptchaGenerator(CreateTemplate()).Generate())
                {
                    captchaRes.Image.SaveAsPng(Path.Combine(currentPath, captchaRes.GetName() + ".png"));
                }
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
            }
        }
    }
}