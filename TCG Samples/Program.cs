global using SixLabors.ImageSharp;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using TCG.Base.Hierarchy;
using TCG.Base.Utils;
using TCG.Drawables;
using TCG.Effects;
using System.Linq;
using TCG.Base.Interfaces;
using TCG.Rnd;

namespace Samples;

class Program
{
    static void Main(string[] args)
    {
        Size canvasSize = new(512, 256);
        Canvas canvas =
            new Canvas(canvasSize)
                .WithLayer(
                    new Layer(canvasSize)
                        .WithDrawable(
                            new DPattern()
                                .WithPattern(pattern =>
                                {
                                    pattern.Width.Value = pattern.Height.Value = 10;
                                })
                                .WithSize(canvasSize))
                        .WithEffect(new Opacity().WithAmount(0.1f)))
                .WithLayer(
                    new Layer(canvasSize)
                        .WithDrawable(
                            new DEllipse()
                                .WithPoint(new Point(256, 128))
                                .WithSize(new Size(100, 100))
                                .WithBrush(brush =>
                                {
                                    brush.WithColor(Color.Green);
                                    brush.WithRandomizedType();
                                })
                                )
                        );
        RandomManager rnd = new(0);
        rnd.RandomizeCanvas(canvas);
        canvas.Render().SaveAsPng(@"D:\Coding\TextCaptcha\TCG Samples\assets\results\test.png");
        //Canvas canvas1 = new Canvas(canvasSize);
        //Layer layer1 = new Layer(canvasSize);
        //DEllipse ellipse = new DEllipse(10, 10, 100, 100)
        //{
        //    Brush =
        //    {
        //        Color =
        //        {
        //            Value = Color.White
        //        },
        //        Type =
        //        {
        //            EnumValues = 
        //        }
        //    }
        //}


        int[] seeds = new[] { 0, 1, 666 };
        //int i = 0;
        //foreach (var image in cg.Generate(TestText(), seeds))
        //{
        //    image.Save($@"D:\Coding\TextCaptcha\TCG Samples\assets\results\gen{seeds[i]}.png");
        //    i++;
        //}

        var template = TestText();

        //TestGenerator.CreateGenerator()
        //    .SetTemplate(template)
        //    .SetSeeds(new[] { 0, 1, 666 })
        //    .Generate();

        //int i = 0;
        //foreach (var captcha in 
        //                        CaptchaGenerator.Create()
        //                            .SetTemplate(TestText())
        //                            .SetSeeds(new[] { 0, 1, 666 })
        //                            .SetCaptchaTexts(new string[] { "test", "TEST", "TeSt" })
        //                            .SetCaptchaSetTextAction((Canvas canvas, string text) =>
        //                            {
        //                                if (canvas.Layers[0].Drawables.FirstOrDefault(x => x is DText) is DText drawableText)
        //                                    drawableText.Text.Value = text;
        //                            }).Generate())
        //{
        //    Console.WriteLine("Save");
        //    captcha.Image.Save($@"D:\Coding\TextCaptcha\TCG Samples\assets\results\gen{seeds[i]}.png");
        //    captcha.Image.SaveAsBmp
        //    i++;
        //}


        var genCaptha = CaptchaGenerator.Create()
            .SetTemplate(template)
            .SetSeeds(new[] { 0, 1, 666 })
            .SetCaptchaTexts(new string[] { "test", "TEST", "TeSt" })
            .SetCaptchaSetTextAction((Canvas canvas, string text) =>
            {
                if (canvas.Layers[0].Drawables.FirstOrDefault(x => x is DText) is DText drawableText)
                    drawableText.Text.Value = text;
            }).Generate();

        CaptchaSaver.Create()
            .SetOutputType(ImageType.Png)
            .SetOutputPath(@"D:\Coding\TextCaptcha\TCG Samples\assets\results\")
            .SaveAsZip("archive")
            .Save(genCaptha);

        //TestMinimal();
        //TestLayers();
    }

    private static void TestMinimal()
    {
        RandomManager rndManager = new(0);
        Canvas canvas = TestText();

        for (int i = 0; i < 5; i++)
        {
            rndManager.ResetRandom(i);
            rndManager.RandomizeCanvas(canvas, false);
            var resImg = canvas.Render();
            resImg.Save($@"D:\Coding\TextCaptcha\TCG Samples\assets\results\gen{i}.png");
        }
    }

    private static Canvas TestText()
    {
        Size canvasSize = new(512, 256);
        Canvas canvas = new(canvasSize);

        var layer0 = new Layer(canvasSize);
        FontCollection collection = new();
        collection.AddSystemFonts();
        var family = collection.Get("Arial", System.Globalization.CultureInfo.InvariantCulture);

        DText text = new (family)
        {
            Point = { Value = new Point((int)(canvasSize.Width / 2f), (int)(canvasSize.Height / 2f)) },
            Pen = { Value = Pens.Solid(Color.Gray, 1f) },
            Brush = { Value = Brushes.Solid(Color.Red) },
            FontSize = { Value = 64},
            Text = { Length = { Value = 4}}
            
        };
        DEllipse ellipse = new()
        {
            Rectangle =
            {
                Point = {
                    X = { Max = canvasSize.Width },
                    Y = { Max = canvasSize.Height }},
                Size = {
                    Width = { Min = 50, Max = 200 },
                    Height = { Min = 50, Max = 200}}
            },
            Type = { Value = DrawableType.Filled }
            
        };
        layer0.Drawables.AddRange(new IDrawable[] { text, ellipse });

        canvas.Layers.AddRange(new[] { layer0 });

        return canvas;
    }

    private static Canvas TestPattern()
    {
        Size canvasSize = new(512, 256);
        Canvas canvas = new(canvasSize);

        var layer0 = new Layer(canvasSize);
        DPattern patter = new()
        {
            Background = { Value = Color.Transparent},
            Rectangle =
            {
                Size = {
                    Width = { Value = canvasSize.Width },
                    Height = { Value = canvasSize.Height }}
            },
            Pattern = {
                Height = { Value = 5 },
                Width = { Value = 5},
            }
        };
        layer0.Drawables.AddRange(new[] { patter });

        canvas.Layers.AddRange(new[] { layer0 });

        return canvas;
    }

    private static Canvas TestRectangle()
    {
        Size canvasSize = new(512, 256);
        Canvas canvas = new(canvasSize);

        var layer0 = new Layer(canvasSize);
        DRectangle rectenagle = new()
        {
            Rectangle =
            {
                Point = {
                    X = { Max = canvasSize.Width },
                    Y = { Max = canvasSize.Height }},
                Size = {
                    Width = { Min = 50, Max = 200 },
                    Height = { Min = 50, Max = 200}}
            },
            Type = { Value = DrawableType.Filled }
        };
        layer0.Drawables.AddRange(new[] { rectenagle });

        canvas.Layers.AddRange(new[] { layer0 });

        return canvas;
    }

    private static Canvas TestPolygon()
    {
        Size canvasSize = new(512, 256);
        Canvas canvas = new(canvasSize);

        var layer0 = new Layer(canvasSize);
        DPolygon polygon = new()
        {
            Points =
            {
                Length = { Min = 3, Max = 10},
                X = { Max = canvasSize.Width },
                Y = { Max = canvasSize.Height },
            },
            Type = { Value = DrawableType.FillWithOutline }
        };
        layer0.Drawables.AddRange(new[] { polygon });

        canvas.Layers.AddRange(new[] { layer0 });

        return canvas;
    }

    private static Canvas TestEllipse()
    {
        Size canvasSize = new(512, 256);
        Canvas canvas = new(canvasSize);

        var layer0 = new Layer(canvasSize);
        DEllipse ellipse = new()
        {
            Rectangle =
            {
                Point = {
                    X = { Max = canvasSize.Width },
                    Y = { Max = canvasSize.Height }},
                Size = {
                    Width = { Min = 50, Max = 200 },
                    Height = { Min = 50, Max = 200}}
            },
            Type = { Value = DrawableType.Filled }
        };
        layer0.Drawables.AddRange(new[] { ellipse });

        canvas.Layers.AddRange(new[] { layer0 });

        return canvas;
    }

    private static Canvas TestLine()
    {
        Size canvasSize = new(512, 256);
        Canvas canvas = new(canvasSize);

        var layer0 = new Layer(canvasSize);
        DLine line = new()
        {
            IsBeziers = {Value = false},
            Pen = { Width = { Min = 5, Max = 10 } },
            Points = { 
                Length = { Value = 2}, 
                X = { Max = canvasSize.Width }, 
                Y = { Min = 128-64, Max = 128 + 64} }
        };
        layer0.Drawables.AddRange(new[] { line });

        canvas.Layers.AddRange(new[] { layer0 });

        return canvas;
    }

    private static void TestLayers()
    {
        RandomManager rndManager = new(0);
        Size canvasSize = new(512, 256);

        Canvas canvas = new(canvasSize);

        var layer0 = new Layer(canvasSize);
        var image = new DImage(@"D:\Coding\TextCaptcha\TCG Samples\assets\img\cat.png");
        image.Point.Value = new Point(256, 128);
        image.Effects.Add(new RgbShift() { Offset = 3 });


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
        layer0.Drawables.Add(new DLine() { Pen = { Value = Pens.Solid(Color.SteelBlue, 7) } });
        //layer0.Effects.Add(new RGBShift(3));
        //layer0.Effects.Add(new Bulge() { X = 340,  Y = 195, Radius = 100, Strenght = 0.1f });
        //layer0.Effects.Add(new HSBCorrection(50, 0, -120));
        //layer0.Effects.Add(new PolarCoordinates() { PolarConversaionType = TCG.Processors.PolarConversionType.CartesianToPolar });
        //layer0.Effects.Add(new PolarCoordinates() { PolarConversaionType = TCG.Processors.PolarConversionType.PolarToCartesian });
        //layer0.Effects.Add(new Ripple() { TraintWidth = 1f });
        //layer0.Effects.Add(new SlitScan() { Time = 1f});
        //layer0.Effects.Add(new Swirl(100, 0, 0.45f) { X = { Value = canvasSize.Width / 2 }, Y = { Value = canvasSize.Height / 2 } });
        //layer0.Effects.Add(new Wave() { WaveType = TCG.Processors.WaveType.Sine, WaveLength = 15});
        //layer0.Effects.Add(new Crystallize() { CrystalsCount = 1024 });
        //layer0.Effects.Add(new Slices() { Count = 10, SliceHeight = 10 });
        //layer0.Effects.Add(new GaussianNoise() { Amount = 255, Monochrome = false});
        //layer0.Effects.Add(new PerlinNoise() { Octaves = 3, Persistence = 2f, Monochrome = false });
        //layer0.Effects.Add(new BoxBlur());

        var layer = new Layer(canvasSize) { BlendPercentage = 1f, ColorBlendingMode = PixelColorBlendingMode.Normal };
        layer.Drawables.Add(new DRectangle(50, 50, 100, 100) { Brush = { Value = Brushes.Solid(Color.Green) } });
        layer.Drawables.Add(new DRectangle(50, 100, 50, 50) { Type = { Value = DrawableType.Outlined }, Pen = { Value = Pens.Dash(Color.Yellow, 5) } });

        var layer2 = new Layer(canvasSize);

        FontCollection collection = new();
        collection.AddSystemFonts();
        collection.TryGet("Arial", out var family);
        DText text = new(family, "TEST");

        //FontRectangle rect = TextMeasurer.Measure("TEST", text.TextOptions);
        text.Point.Value = new Point((int)(512 / 2), (int)(256 / 2));
        layer2.Drawables.Add(text);
        //layer2.Effects.Add(new Wave() { WaveType = TCG.Processors.WaveType.Sine, WaveLength = 6 });


        var layer3 = new Layer(canvasSize) { BlendPercentage = 0.5f };
        layer3.Drawables.Add(new DEllipse(256, 128, 250, 250)
        {
            Type = { Value = DrawableType.FillWithOutline },
            Brush = { Value = Brushes.Solid(Color.DeepPink) },
            Pen = { Value = Pens.Dot(Color.Orange, 3) }
        });

        var layer4 = new Layer(new Size(600, 600)) { BlendPercentage = 1f, ColorBlendingMode = PixelColorBlendingMode.Normal };
        //layer.Drawables.Add(new DRectangle(0, 0, 600, 600) { Type = TCG.Base.Abstract.DrawableType.Filled, Brush = Brushes.Solid(Color.FromRgba(0, 0, 0, 255)) });
        //layer.Effects.Add(new GaussianNoise() { Amount = { Value = 255 } });

        canvas.Layers.AddRange(new Layer[] { layer0, layer, layer2, layer3, layer4 });

        rndManager.RandomizeCanvas(canvas, true);

        var resImg = canvas.Render();
        resImg.Save(@"D:\Coding\TextCaptcha\TCG Samples\assets\results\gen.png");
    }
}
