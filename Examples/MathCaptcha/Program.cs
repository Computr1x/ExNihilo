using ExNihilo.Base;
using ExNihilo.Effects;
using ExNihilo.Utils;
using ExNihilo.Visuals;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using System.Globalization;

FontFamily fontFamily = new FontCollection().AddSystemFonts().GetByCulture(CultureInfo.CurrentCulture).First();

Size containerSize = new(512, 256);
Point leftSide = new(256 - 128, 128);
Point rightSide = new(256 + 128, 128);
Point center = new(256, 128);
// create and render container
Container container = new Container(containerSize)
    .WithContainer(
        new Container(containerSize)
            .WithBackground(Color.White)
            .WithChild(
                new Captcha()
                    .WithIndex(0)
                    .WithPoint(leftSide)
                    .WithFontSize(100)
                    .WithRandomizedContent(content =>
                    {
                        content.WithRandomizedLength(1, 2);
                        content.WithCharactersSet(StringProperty.decdigits);
                    })
                    .WithPen((PenProperty pen) =>
                    {
                        pen.WithRandomizedWidth(1, 3);
                    })
                    .WithRandomizedBrush(10)
                    .WithFontFamily(fontFamily)
                    .WithType(VisualType.Filled))
            .WithChild(
                new Captcha()
                    .WithIndex(1)
                    .WithPoint(center)
                    .WithFontSize(100)
                    .WithRandomizedContent(content =>
                    {
                        content.WithLength(1);
                        content.WithCharactersSet("+-".ToCharArray());
                    })
                    .WithRandomizedBrush(10)
                    .WithPen((PenProperty pen) =>
                    {
                        pen.WithWidth(1);
                    })
                    .WithFontFamily(fontFamily))
            .WithChild(
                new Captcha()
                    .WithIndex(2)
                    .WithPoint(rightSide)
                    .WithFontSize(100)
                    .WithRandomizedContent(content =>
                    {
                        content.WithRandomizedLength(1, 2);
                        content.WithCharactersSet(StringProperty.decdigits);
                    })
                    .WithPen((PenProperty pen) =>
                    {
                        pen.WithRandomizedWidth(1,3);
                    })
                    .WithRandomizedBrush(10)
                    .WithFontFamily(fontFamily))
            .WithEffect(
                new Wave()
                    .WithWaveWaveType(ExNihilo.Processors.WaveType.Sine)
                    .WithRandomizedAmplitude(5, 10)
                    .WithRandomizedWaveLength(100, 150))
    );

// lazy generation of three captchas
var captchaResults = new ImageGenerator(container).WithSeedsCount(10).Generate();
// save captcha as separate files
new ImageSaver(captchaResults).WithOutputPath("./").CreateFolder("Results").Save();