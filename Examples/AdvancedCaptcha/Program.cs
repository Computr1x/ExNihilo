using ExNihilo.Base;
using ExNihilo.Effects;
using ExNihilo.Utils;
using ExNihilo.Visuals;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using System.Globalization;

FontFamily fontFamily = new FontCollection().AddSystemFonts().GetByCulture(CultureInfo.CurrentCulture).First();
FontCollection collection = new FontCollection();
collection.Add("./fonts/Momоt.ttf");
collection.Add("./fonts/Norefund.ttf");
var zxxFont = collection.Add("./fonts/zxx.ttf");

Size containerSize = new(512, 256);
Point center = new(256, 128);

// create container
Container container = new Container(containerSize)
    .WithContainer(
        new Container(containerSize)
            .WithBackground(Color.White)
            .WithChildren(
                Enumerable.Range(0, 7).Select(x =>
                    new Text()
                        .WithRandomizedPoint(0, 512, 0, 256)
                        .WithFontFamily(zxxFont)
                        .WithRandomizedBrush(100)
                        .WithRandomizedFontSize(80, 100)
                        .WithType(VisualType.Filled)
                        .WithRandomizedContent((StringProperty str) =>
                        {
                            str.CharactersSet = StringProperty.asciiLetters;
                            str.WithLength(1);
                        })
                        .WithEffect(new Rotate().WithRandomizedDegree(-30, 30))))
            .WithEffect(new RgbShift(3))
            .WithEffect(new GaussianNoise(155, false)))
    .WithContainer(
        new Container(containerSize)
            .WithChild(
                new CaptchaSymbols()
                    .WithPoint(center)
                    .WithFontSize(100)
                    .WithRandomizedContent(content =>
                    {
                        content.WithLength(5);
                        content.WithCharactersSet(StringProperty.asciiUpperCase);
                    })
                    .WithRandomizedBrush(10)
                    .WithRandomizedFontFamily(collection.Families)
                    .WithType(VisualType.Filled)
                    .WithSymbolsEffect(new Rotate().WithRandomizedDegree(-15,15))
            .WithEffect(
                    new Wave()
                        .WithWaveWaveType(ExNihilo.Processors.WaveType.Sine)
                        .WithRandomizedAmplitude(5, 10)
                        .WithRandomizedWaveLength(100, 150))
            ))
    .WithContainer(
        new Container(containerSize)
            .WithChild(
                new Line()
                    .WithRandomizedPoints(15, 0, 512, 0, 256)
                    ))
    .WithContainer(
        new Container(containerSize)
            .WithColorBlendingMode(SixLabors.ImageSharp.PixelFormats.PixelColorBlendingMode.Multiply)
            .WithChildren(
                Enumerable.Range(0, 8).Select(x =>
                    new Ellipse()
                        .WithRandomizedPoint(0, 512, 0, 256)
                        .WithRandomizedSize(50, 80)
                        .WithType(VisualType.Filled)
                        .WithBrush((BrushProperty brush) =>
                        {
                            brush.WithRandomizedColor(32);
                            brush.WithType(BrushType.Solid);
                        })
                )))
    ;

// lazy generation of three captchas
var captchaResults = new ImageGenerator(container).WithSeedsCount(15).Generate();
// save captcha as separate files
new ImageSaver(captchaResults).WithOutputPath("./").CreateFolder("Results").Save();