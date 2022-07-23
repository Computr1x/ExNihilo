global using SixLabors.ImageSharp;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Hierarchy;
using TCG.Drawables;
using TCG.Effects;
using TCG.Extensions.Processors;

namespace Samples;

class Program
{
    static void Main(string[] args)
    {
        //TestShapeCreation();
        //TestEffect();
        TestLayers();
    }

    private static void TestLayers()
    {
        Canvas canvas = new Canvas(512, 256);

        var layer0 = canvas.CreateLayer();
        var image = new DImage(@"D:\Coding\TextCaptcha\TCG Samples\assets\img\cat.png") { Location = new Point(256, 128) };
        image.Effects.Add(new RGBShift(3));
        layer0.Drawables.Add(image);
        layer0.Drawables.Add(new DLine(new PointF[] { new PointF(20, 20), new PointF(256, 128), new PointF(300, 50), new PointF(512, 256) }) { IsBeziers = false, Pen = Pens.Solid(Color.Brown, 8) });
        //layer0.Effects.Add(new RGBShift(3));
        //layer0.Effects.Add(new Bulge(340, 195, 100, 0.1f));
        //layer0.Effects.Add(new HSBCorrection(50, 0, -120));
        //layer0.Effects.Add(new PolarCoordinates() { PolarConversaionType = TCG.Processors.PolarConversionType.CartesianToPolar });
        //layer0.Effects.Add(new PolarCoordinates() { PolarConversaionType = TCG.Processors.PolarConversionType.PolarToCartesian });
        //layer0.Effects.Add(new Ripple() { TraintWidth = 1f });
        //layer0.Effects.Add(new SlitScan() { Time = 1f});
        //layer0.Effects.Add(new Swirl() { Radius = 100f, Twists = 0.25f});
        //layer0.Effects.Add(new Wave() { WaveType = TCG.Processors.WaveType.Sine, WaveLength = 15});
        //layer0.Effects.Add(new Crystallize() { CrystalsCount = 1024 });
        layer0.Effects.Add(new Slices() { Count = 10, SliceHeight = 10 });
        //layer0.Effects.Add(new );

        var layer = canvas.CreateLayer();
        layer.Drawables.Add(new DRectangle(50, 50, 100, 100) { Brush = Brushes.Solid(Color.Green) });
        layer.Drawables.Add(new DRectangle(50, 100, 50, 50) { Type = TCG.Base.Abstract.DrawableType.Outlined, Pen = Pens.Dash(Color.Yellow, 5) });

        var layer2 = canvas.CreateLayer();

        FontCollection collection = new();
        collection.AddSystemFonts();
        collection.TryGet("Arial", out var family);
        Font font = family.CreateFont(64);
        DText text = new(font, "TEST");
        text.TextOptions.TextAlignment = TextAlignment.Center;
        FontRectangle rect = TextMeasurer.Measure("TEST", text.TextOptions);
        text.TextOptions.Origin = new System.Numerics.Vector2(512 / 2 - rect.Width / 2, 256 / 2 - rect.Height / 2);
        layer2.Drawables.Add(text);


        var layer3 = canvas.CreateLayer(new GraphicsOptions() { BlendPercentage = 0.5f });
        layer3.Drawables.Add(new DEllipse(256, 128, 250, 250) { Type = TCG.Base.Abstract.DrawableType.FillWithOutline, Brush = Brushes.Solid(Color.DeepPink), Pen = Pens.Dot(Color.Orange, 3) });


        var resImg = canvas.Render();
        resImg.Save(@"D:\Coding\TextCaptcha\TCG Samples\assets\results\gen.png");
    }

    private static void TestShapeCreation()
    {
        //Image image = Image.Load(@"D:\Coding\TextCaptcha\TextCaptcha\data\test.png");
        Image image = new Image<Rgba32>(512, 256);

        IPath path = new RectangularPolygon(20, 20, 200, 100);
        image.Mutate((x) =>
        {
            x.Fill(Color.Green, path);
            //x.Draw(Color.Blue, 3, path);
        });

        var dopt = new DrawingOptions()
        {
            GraphicsOptions = new GraphicsOptions() { BlendPercentage = 0.3f, ColorBlendingMode = PixelColorBlendingMode.Normal },
        };
        IPath ellipse = new EllipsePolygon(50, 50, 100);

        image.Mutate((x) =>
        {
            x.Fill(dopt, new Color(new Abgr32(200, 200, 200)), ellipse);
            //x.Draw(dopt, Color.Azure, 1, ellipse);


            //x.Grayscale();
            x.RGBShift(3);
            //x.ApplyProcessor(new ImagePro)
        });



        image.Save(@"D:\Coding\TextCaptcha\TextCaptcha\data\test1.png");
    }

    private static void TestEffect()
    {
        Image image = Image.Load(@"D:\Coding\TextCaptcha\TextCaptcha\data\img.png");



        image.Mutate((x) =>
        {
            //x.BoxBlur()
            x.RGBShift(3, new Rectangle(40, 40, 40, 40));
        });
        image.Save(@"D:\Coding\TextCaptcha\TextCaptcha\data\img1.png");
    }
}
