using SixLabors.Fonts;
using SixLabors.ImageSharp;
using ExNihilo.Base.Hierarchy;
using ExNihilo.Drawables;
using ExNihilo.Rnd;
using ExNihilo.Effects;
using ExNihilo.Base.Parameters;
using System.Globalization;

FontFamily fontFamily = new FontCollection().AddSystemFonts().GetByCulture(CultureInfo.CurrentCulture).First();
FontCollection collection = new FontCollection();
collection.Add("./fonts/Momоt.ttf");
collection.Add("./fonts/Norefund.ttf");
var zxxFont = collection.Add("./fonts/zxx.ttf");

Size canvasSize = new(512, 256);
Point center = new(256, 128);

// create canvas
Canvas canvas = new Canvas(canvasSize)
    .WithLayer(
        new Layer(canvasSize)
            .WithBackground(Color.White)
            .WithDrawables(
                Enumerable.Range(0, 7).Select(x =>
                    new Text()
                        .WithRandomizedPoint(0, 512, 0, 256)
                        .WithFontFamily(zxxFont)
                        .WithRandomizedBrush(100)
                        .WithRandomizedFontSize(80, 100)
                        .WithType(ExNihilo.Base.Utils.DrawableType.Filled)
                        .WithRandomizedContent((StringParameter str) =>
                        {
                            str.CharactersSet = StringParameter.asciiLetters;
                            str.WithLength(1);
                        })
                        .WithEffect(new Rotate().WithRandomizedDegree(-30, 30))))
            .WithEffect(new RgbShift(3))
            .WithEffect(new GaussianNoise(155, false))
            )
    .WithLayer(
        new Layer(canvasSize)
            .WithDrawable(
                new CaptchaSymbols()
                    .WithPoint(center)
                    .WithFontSize(100)
                    .WithRandomizedContent(content =>
                    {
                        content.WithLength(5);
                        content.WithCharactersSet(StringParameter.asciiUpperCase);
                    })
                    .WithRandomizedBrush(10)
                    .WithRandomizedFontFamily(collection.Families)
                    .WithType(ExNihilo.Base.Utils.DrawableType.Filled)
                    .WithSymbolsEffect(new Rotate().WithRandomizedDegree(-15,15))
            .WithEffect(
                    new Wave()
                        .WithWaveWaveType(ExNihilo.Processors.WaveType.Sine)
                        .WithRandomizedAmplitude(5, 10)
                        .WithRandomizedWaveLength(100, 150))
            ))
    .WithLayer(
        new Layer(canvasSize)
            .WithDrawable(
                new Line()
                    .WithRandomizedPoints(15, 0, 512, 0, 256)
                    ))
    .WithLayer(
        new Layer(canvasSize)
            .WithColorBlendingMode(SixLabors.ImageSharp.PixelFormats.PixelColorBlendingMode.Multiply)
            .WithDrawables(
                Enumerable.Range(0, 8).Select(x =>
                    new Ellipse()
                        .WithRandomizedPoint(0, 512, 0, 256)
                        .WithRandomizedSize(50, 80)
                        .WithType(ExNihilo.Base.Utils.DrawableType.Filled)
                        .WithBrush((BrushParameter brush) =>
                        {
                            brush.WithRandomizedColor(32);
                            brush.WithType(ExNihilo.Base.Utils.BrushType.Solid);
                        })
                )))
    ;

// lazy generation of three captchas
var captchaResults = new ImageGenerator(canvas).WithSeedsCount(15).Generate();
// save captcha as separate files
new ImageSaver(captchaResults).WithOutputPath("./").CreateFolder("Results").Save();