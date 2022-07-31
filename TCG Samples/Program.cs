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
using TCG.Rnd.Managers;

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
        RNDManager rndManager = new(0);
        Size canvasSize = new(512, 256);

        Canvas canvas = new(canvasSize);

        var layer0 = new Layer(canvasSize);
        var image = new DImage(@"D:\Coding\TextCaptcha\TCG Samples\assets\img\cat.png");
        image.Location.Value = new Point(256, 128);
        image.Effects.Add(new RGBShift() { Offset = 3 });


        layer0.Drawables.Add(
            new DPattern(
                new Rectangle(0, 0, 512, 256),
                new bool[,] {
                    { false, false, true, false, false, false, true, false, false, false },
                    { false, true, true, true, false, true, true, true, false, false },
                    { true, true, true, true, true, true, true, true, true, false },
                    { true, true, true, true, true, true, true, true, true, false },
                    { false, true, true, true, true, true, true, true, false, false },
                    { false, false, true, true, true, true, true, false, false, false },
                    { false, false, false, true, true, true, false, false, false, false },
                    { false, false, false, false, true, false, false, false, false, false }},
                Color.Transparent, Color.Gainsboro));
        layer0.Drawables.Add(image);
        //new PointF[] { new PointF(20, 20), new PointF(256, 128), new PointF(300, 50), new PointF(512, 256) }
        layer0.Drawables.Add(new DLine() { Pen = { Value = Pens.Solid(Color.SteelBlue, 7) } } );
        //layer0.Effects.Add(new RGBShift(3));
        //layer0.Effects.Add(new Bulge() { X = 340,  Y = 195, Radius = 100, Strenght = 0.1f });
        //layer0.Effects.Add(new HSBCorrection(50, 0, -120));
        //layer0.Effects.Add(new PolarCoordinates() { PolarConversaionType = TCG.Processors.PolarConversionType.CartesianToPolar });
        //layer0.Effects.Add(new PolarCoordinates() { PolarConversaionType = TCG.Processors.PolarConversionType.PolarToCartesian });
        //layer0.Effects.Add(new Ripple() { TraintWidth = 1f });
        //layer0.Effects.Add(new SlitScan() { Time = 1f});
        layer0.Effects.Add(new Swirl(100, 0, 0.45f) { X = { Value = canvasSize.Width / 2 }, Y = { Value = canvasSize.Height / 2 } });
        //layer0.Effects.Add(new Wave() { WaveType = TCG.Processors.WaveType.Sine, WaveLength = 15});
        //layer0.Effects.Add(new Crystallize() { CrystalsCount = 1024 });
        //layer0.Effects.Add(new Slices() { Count = 10, SliceHeight = 10 });
        //layer0.Effects.Add(new GaussianNoise() { Amount = 255, Monochrome = false});
        //layer0.Effects.Add(new PerlinNoise() { Octaves = 3, Persistence = 2f, Monochrome = false });
        //layer0.Effects.Add(new BoxBlur());

        var layer = new Layer(canvasSize) { BlendPercentage = 1f, ColorBlendingMode = PixelColorBlendingMode.Normal};
        layer.Drawables.Add(new DRectangle(50, 50, 100, 100) { Brush = Brushes.Solid(Color.Green) });
        layer.Drawables.Add(new DRectangle(50, 100, 50, 50) { Type = TCG.Base.Abstract.DrawableType.Outlined, Pen = Pens.Dash(Color.Yellow, 5) });

        var layer2 = new Layer(canvasSize);

        FontCollection collection = new();
        collection.AddSystemFonts();
        collection.TryGet("Arial", out var family);
        Font font = family.CreateFont(64);
        DText text = new(font, "TEST");

        //FontRectangle rect = TextMeasurer.Measure("TEST", text.TextOptions);
        text.Origin.Value = new Point((int)(512 / 2), (int)(256 / 2));
        layer2.Drawables.Add(text);
        //layer2.Effects.Add(new Wave() { WaveType = TCG.Processors.WaveType.Sine, WaveLength = 6 });


        var layer3 = new Layer(canvasSize) { BlendPercentage = 0.5f};
        layer3.Drawables.Add(new DEllipse(256, 128, 250, 250) { Type = TCG.Base.Abstract.DrawableType.FillWithOutline, Brush = Brushes.Solid(Color.DeepPink), Pen = Pens.Dot(Color.Orange, 3) });

        var layer4 = new Layer(new Size(600, 600)) { BlendPercentage =1f, ColorBlendingMode = PixelColorBlendingMode.Normal };
        //layer.Drawables.Add(new DRectangle(0, 0, 600, 600) { Type = TCG.Base.Abstract.DrawableType.Filled, Brush = Brushes.Solid(Color.FromRgba(0, 0, 0, 255)) });
        //layer.Effects.Add(new GaussianNoise() { Amount = { Value = 255 } });

        canvas.Layers.AddRange(new Layer[] { layer0, layer, layer2, layer3, layer4 });

        rndManager.RandomizeCanvas(canvas, true);

        var resImg = canvas.Render();
        resImg.Save(@"D:\Coding\TextCaptcha\TCG Samples\assets\results\gen.png");
    }
}
