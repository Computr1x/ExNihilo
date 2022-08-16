namespace ExNihilo.Tests;

[TestClass]
public class EffectTest : Test
{
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
            .WithBackground(Color.White)
            .WithChild(
                new Pattern()
                    .WithArea(new SixLabors.ImageSharp.Rectangle(0, 0, 512, 256))
                    .WithTemplate(template)
                    .WithRandomizedForegroundColor(10, 100)
                    .WithBackgroundColor(Color.Transparent)
            );
    }

    [TestMethod]
    public void TestGeneratorWithSeeds()
    {
        Swirl effect = new(new Point(256, 128), 75, 10, 0.15f);
        Container template = CreateTemplate();
        template.Children[0].WithEffect(effect);

        foreach (var captchaRes in new ImageGenerator(template).WithSeedsCount(3).Generate())
            captchaRes.Image.SaveAsPng(Path.Combine(CurrentPath, $"{effect.GetType().Name}{captchaRes.GetName()}.png"));
    }
}
