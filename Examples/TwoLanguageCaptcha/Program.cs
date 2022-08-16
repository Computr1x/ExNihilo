using SixLabors.Fonts;
using SixLabors.ImageSharp;
using ExNihilo.Base.Hierarchy;
using ExNihilo.Drawables;
using ExNihilo.Rnd;
using ExNihilo.Effects;
using ExNihilo.Base.Parameters;

var koreanFontFamily = new FontCollection().Add("./fonts/NotoSerifKR-Regular.otf");
var englishFontFamily = new FontCollection().Add("./fonts/NotoSans-Regular.ttf");

Size canvasSize = new(512, 256);
Point leftSide = new(256-128, 128);
Point rightSide = new(256+128, 128);

int size = 10;
bool[,] template = new bool[size, size];
Enumerable.Range(0, template.GetLength(0)).ToList().ForEach((index) =>
{
    template[index, 0] = true;
    template[0, index] = true;
});
// create and render canvas
Canvas canvas = new Canvas(canvasSize)
    .WithLayer(
        new Layer(canvasSize)
            .WithBackground(Color.White)
            .WithDrawable(
                new Pattern()
                    .WithSize(canvasSize)
                    .WithTemplate(template)
                    .WithForegroundColor(Color.Black)
                    .WithBackgroundColor(Color.Transparent))
            .WithEffect(new Swirl(new Point(256, 128), 150, 10, 0.15f))
    )
    .WithLayer(
        new Layer(canvasSize)
            .WithDrawable(
                new Captcha()
                    .WithIndex(0)
                    .WithPoint(leftSide)
                    .WithFontSize(75)
                    .WithRandomizedContent(content =>
                    {
                        content.WithLength(5);
                        content.WithCharactersSet(StringParameter.asciiUpperCase);
                    })
                    .WithRandomizedBrush(10)
                    .WithFontFamily(englishFontFamily)
                    .WithType(ExNihilo.Base.Utils.DrawableType.Filled))
            .WithDrawable(
                new Captcha()
                    .WithIndex(1)
                    .WithPoint(rightSide)
                    .WithFontSize(75)
                    .WithRandomizedBrush(10)
                    .WithFontFamily(koreanFontFamily)
                    .WithType(ExNihilo.Base.Utils.DrawableType.Filled))
    );

// lazy generation of three captchas
// left part is a randomly generated english characters
// right part is korean words
var captchaResults = new ImageGenerator(canvas).WithCaptchaInput(new string[] { "거북이", "고래", "승리" }, 1).Generate();
// save captcha as separate files
new ImageSaver(captchaResults).WithOutputPath("./").CreateFolder("Results").Save();