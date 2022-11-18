using ExNihilo.Base;
using ExNihilo.Utils;
using ExNihilo.Visuals;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using System.Globalization;

var fontFamily = new FontCollection().AddSystemFonts().GetByCulture(CultureInfo.CurrentCulture).First();
Size containerSize = new(512, 256);
Container container = new Container(containerSize)
    .WithContainer(
        new Container(containerSize)
            .WithBackground(Color.White)
            .WithChild(
                new Captcha()
                    .WithPoint(new Point(256,128))
                    .WithFontSize(100)
                    .WithRandomizedContent(content =>
                    { content.WithLength(5); content.WithCharactersSet(StringProperty.asciiUpperCase);})
                    .WithRandomizedBrush(10)
                    .WithFontFamily(fontFamily)
                    .WithType(VisualType.Filled)));

new ImageSaver(new ImageGenerator(container).WithSeedsCount(3).Generate()).
    WithOutputPath("./").CreateFolder("Results").Save();