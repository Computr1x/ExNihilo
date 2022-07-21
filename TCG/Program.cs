global using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing.Processors;
using TCG.Base.Hierarchy;
using TCG.Drawables;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;

namespace TCG;

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
        IDrawable drawable;

        Canvas canvas = new Canvas(512, 256);

        var layer = canvas.CreateLayer();
        layer.Drawables.Add(new DRectangle(50, 50, 100, 100) { Brush = Brushes.Solid(Color.Green) });
        layer.Drawables.Add(new DRectangle(50, 100, 50, 50) { Type = Base.Abstract.DrawableType.Outlined,  Pen = Pens.Dash(Color.Yellow, 5) });

        var layer2 = canvas.CreateLayer(new GraphicsOptions() { BlendPercentage = 0.5f });
        layer2.Drawables.Add(new DEllipse(256, 128, 250, 250) { Type = Base.Abstract.DrawableType.FillWithOutline, Brush = Brushes.Solid(Color.DeepPink), Pen = Pens.Dot(Color.Orange, 3) });

        var resImg = canvas.Render();
        resImg.Save(@"D:\Coding\TextCaptcha\TCG\data\gen.png");
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
            x.Fill(dopt, new Color(new Abgr32(200,200,200)), ellipse);
            //x.Draw(dopt, Color.Azure, 1, ellipse);
            

            //x.Grayscale();
            x.RGBShift(3);
            //x.ApplyProcessor(new ImagePro)
        });
        
        

        image.Save(@"D:\Coding\TextCaptcha\TextCaptcha\data\test1.png");
    }

    private static void TestEffect()
    {
        IImageProcessor processor;
        

        Image image = Image.Load(@"D:\Coding\TextCaptcha\TextCaptcha\data\img.png");

        

        image.Mutate((x) =>
        {
            //x.BoxBlur()
            x.RGBShift(3, new Rectangle(40,40,40,40));
        });
        image.Save(@"D:\Coding\TextCaptcha\TextCaptcha\data\img1.png");
    }
}
