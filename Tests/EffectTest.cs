using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using ExNihilo.Base.Hierarchy;
using ExNihilo.Base.Interfaces;
using ExNihilo.Base.Utils;
using ExNihilo.Drawables;
using ExNihilo.Effects;
using ExNihilo.Rnd;

namespace ExNihilo.Tests
{
    [TestClass]
    public class EffectTest
    {
        static string currentPath;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            currentPath = Path.Combine(Directory.GetCurrentDirectory(), "EffectsTest");
            if (Directory.Exists(currentPath))
                Directory.Delete(currentPath, true);
            Directory.CreateDirectory(currentPath);
        }

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

            return new Container(containerSize)
                .WithContainer(new Container(containerSize).WithBackground(Color.White))
                .WithContainer(
                    new Container(containerSize)
                        .WithChild(
                            new Pattern()
                                .WithArea(new SixLabors.ImageSharp.Rectangle(0,0, 512, 256))
                                .WithTemplate(template)
                                .WithRandomizedForegroundColor(10, 100)
                                .WithBackgroundColor(Color.Transparent))
                );
        }

        private static void TestEffect(Effect effect)
        {
            Container template = CreateTemplate();
            template.Children[1].WithEffect(effect);

            foreach (var captchaRes in new ImageGenerator(template).WithSeedsCount(3).Generate())
            {
                captchaRes.Image.SaveAsPng(Path.Combine(currentPath, effect.GetType().Name + captchaRes.GetName() + ".png"));
            }
        }

        [TestMethod]
        public void TestGeneratorWithSeeds()
        {
            Effect effect = new Swirl(new Point(256, 128), 75, 10, 0.15f);
            TestEffect(effect);
        }
    }
}