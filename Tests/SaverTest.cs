using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using ExNihilo.Base.Hierarchy;
using ExNihilo.Base.Utils;
using ExNihilo.Drawables;
using ExNihilo.Rnd;

namespace ExNihilo.Tests
{
    [TestClass]
    public class SaverTest
    {
        static string currentPath;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            currentPath = Path.Combine(Directory.GetCurrentDirectory(), "SaverTest");
            if (Directory.Exists(currentPath))
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
        public void TestSaveAsSeparateFiles()
        {
            var captchaResults = new ImageGenerator(CreateTemplate()).WithSeedsCount(5).Generate();

            new ImageSaver(captchaResults).WithOutputPath(currentPath).CreateFolder("TestFolder1").WithFilePrefix("_test_").WithOutputType(ImageType.Png).Save();
            new ImageSaver(captchaResults).WithOutputPath(Path.Join(currentPath, "TestFolder1")).WithFilePrefix("_test_").WithOutputType(ImageType.Jpeg).Save();
        }

        [TestMethod]
        public void TestSaveAsZip()
        {
            var captchaResults = new ImageGenerator(CreateTemplate()).WithSeedsCount(5).Generate();

            new ImageSaver(captchaResults).WithOutputPath(currentPath).SaveAsZip();
            new ImageSaver(captchaResults).WithOutputPath(currentPath).WithFilePrefix("_test_").WithOutputType(ImageType.Jpeg).SaveAsZip("archive2");
        }
    }
}