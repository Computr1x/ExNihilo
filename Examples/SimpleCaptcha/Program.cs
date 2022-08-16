using SixLabors.Fonts;
using SixLabors.ImageSharp;
using ExNihilo.Base.Hierarchy;
using ExNihilo.Drawables;
using ExNihilo.Rnd;
using ExNihilo.Effects;
using ExNihilo.Base.Parameters;
using System.Globalization;

//FontFamily fontFamily = new FontCollection().AddSystemFonts().GetByCulture(CultureInfo.CurrentCulture).First();

//Size canvasSize = new(512, 256);
//Point center = new(256, 128);
//// create and render canvas
//Canvas canvas = new Canvas(canvasSize)
//    .WithLayer(
//        new Layer(canvasSize)
//            .WithBackground(Color.White)
//            .WithDrawable(
//                new Captcha()
//                    .WithPoint(center)
//                    .WithFontSize(100)
//                    .WithRandomizedContent(content =>
//                    {
//                        content.WithLength(5);
//                        content.WithCharactersSet(StringParameter.asciiUpperCase);
//                    })
//                    .WithRandomizedBrush(10)
//                    .WithFontFamily(fontFamily)
//                    .WithType(ExNihilo.Base.Utils.DrawableType.Filled))
//    );

//// lazy generation of three captchas
//var captchaResults = new ImageGenerator(canvas).WithSeedsCount(3).Generate();
//// save captcha as separate files
//new ImageSaver(captchaResults).WithOutputPath("./").CreateFolder("Results").Save();



var fontFamily = new FontCollection().AddSystemFonts().GetByCulture(CultureInfo.CurrentCulture).First();
Size canvasSize = new(512, 256);
Canvas canvas = new Canvas(canvasSize)
    .WithLayer(
        new Layer(canvasSize)
            .WithBackground(Color.White)
            .WithDrawable(
                new Captcha()
                    .WithPoint(new Point(256,128))
                    .WithFontSize(100)
                    .WithRandomizedContent(content =>
                    { content.WithLength(5); content.WithCharactersSet(StringParameter.asciiUpperCase);})
                    .WithRandomizedBrush(10)
                    .WithFontFamily(fontFamily)
                    .WithType(ExNihilo.Base.Utils.DrawableType.Filled)));

new ImageSaver(new ImageGenerator(canvas).WithSeedsCount(3).Generate()).
    WithOutputPath("./").CreateFolder("Results").Save();