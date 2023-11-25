using ExNihilo.Base;
using ExNihilo.Effects;
using ExNihilo.Utils;
using ExNihilo.Visuals;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

var koreanFontFamily = new FontCollection().Add("./fonts/NotoSerifKR-Regular.otf");
var englishFontFamily = new FontCollection().Add("./fonts/NotoSans-Regular.ttf");

Size containerSize = new(512, 256);
Point leftSide = new(256-128, 128);
Point rightSide = new(256+128, 128);

int size = 10;
bool[,] template = new bool[size, size];
Enumerable.Range(0, template.GetLength(0)).ToList().ForEach((index) =>
{
    template[index, 0] = true;
    template[0, index] = true;
});
// create and render container
Container container = new Container(containerSize)
    .WithContainer(
        new Container(containerSize)
            .WithBackground(Color.White)
            .WithChild(
                new Pattern()
                    .WithSize(containerSize)
                    .WithTemplate(template)
                    .WithForegroundColor(Color.Black)
                    .WithBackgroundColor(Color.Transparent))
            .WithEffect(new Swirl(new Point(256, 128), 150, 10, 0.15f))
    )
    .WithContainer(
        new Container(containerSize)
            .WithChild(
                new Captcha()
                    .WithIndex(0)
                    .WithPoint(leftSide)
                    .WithFontSize(75)
                    .WithRandomizedContent(content =>
                    {
                        content.WithLength(5);
                        content.WithCharactersSet(StringProperty.asciiUpperCase);
                    })
                    .WithRandomizedBrush(10)
                    .WithFontFamily(englishFontFamily)
                    .WithType(VisualType.Filled))
            .WithChild(
                new Captcha()
                    .WithIndex(1)
                    .WithPoint(rightSide)
                    .WithFontSize(75)
                    .WithRandomizedBrush(10)
                    .WithFontFamily(koreanFontFamily)
                    .WithType(VisualType.Filled))
    );

// lazy generation of three captchas
// left part is a randomly generated english characters
// right part is korean words
var captchaResults = new CaptchaGenerator(container).WithCaptchaInput(new string[] { "거북이", "고래", "승리" }, 1).Generate();
// save captcha as separate files
new ImageSaver(captchaResults).WithOutputPath("./").CreateFolder("Results").Save();