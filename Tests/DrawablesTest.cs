using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using ExNihilo.Base.Hierarchy;
using ExNihilo.Base.Utils;
using ExNihilo.Drawables;

namespace ExNihilo.Tests
{
    [TestClass]
    public class DrawablesTest
    {
        Size canvasSize = new(512, 256);
        Point center = new(256, 128);
        Canvas canvas;
        string currentPath = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "DrawablesTest")).FullName;

        [TestInitialize]
        public void InitCanvas()
        {
            canvas = new Canvas(canvasSize).WithLayer(new Layer(canvasSize).WithBackground(Color.Orange));
        }

        [TestMethod]
        public void TestEllipse()
        {
            Ellipse ellipse =
                new Ellipse()
                    .WithPoint(center)
                    .WithSize(new Size(100))
                    .WithBrush(brush =>
                    {
                        brush.WithType(BrushType.Horizontal);
                        brush.WithColor(Color.Green);
                    })
                    .WithPen(pen =>
                    {
                        pen.WithColor(Color.Black);
                        pen.WithWidth(3);
                    })
                    .WithType(DrawableType.FillWithOutline);

            canvas.Layers[0].WithDrawable(ellipse).Render().Save(Path.Join(currentPath, "ellipse.png"));
        }

        [TestMethod]
        public void TestLine()
        {
            Line line =
                new Line()
                    .WithPoints(new PointF[] { new PointF(0, 0), new PointF(512, 256) })
                    .WithPen(pen =>
                    {
                        pen.WithColor(Color.Blue);
                        pen.WithWidth(3);
                        pen.WithType(PenType.Dash);
                    });

            canvas.Layers[0].WithDrawable(line).Render().Save(Path.Join(currentPath, "line.png"));
        }

        [TestMethod]
        public void TestPattern()
        {
            Pattern pattern =
                new Pattern()
                    .WithTemplate(new bool[,] { { true, false, false, false }, { false, false, false, false }, { false, false, false, false } })
                    .WithSize(canvasSize)
                    .WithBackgroundColor(Color.Transparent)
                    .WithForegroundColor(Color.Gray);
            canvas.Layers[0].WithDrawable(pattern).Render();
        }

        [TestMethod]
        public void TestPicture()
        {
            Picture picture = new(@"./Assets/Images/cat.png");
            canvas.Layers[0].WithDrawable(picture).Render().Save(Path.Join(currentPath, "picture.png"));
        }

        [TestMethod]
        public void TestPolygon()
        {
            Polygon polygon =
                new Polygon()
                    .WithPoints(new PointF[] { new(400, 200), new(350, 150), new(300, 179) })
                    .WithBrush(BrushType.Solid, Color.Peru)
                    .WithType(DrawableType.Filled);
            canvas.Layers[0].WithDrawable(polygon).Render().Save(Path.Join(currentPath, "polygon.png"));
        }

        [TestMethod]
        public void TestRectangle()
        {
            Drawables.Rectangle rectangle =
                new Drawables.Rectangle(50, 200, 100, 50)
                .WithPen(PenType.Dash, 1, Color.Olive)
                .WithType(DrawableType.Outlined);
            canvas.Layers[0].WithDrawable(rectangle).Render().Save(Path.Join(currentPath, "rectangle.png"));
        }

        [TestMethod]
        public void TestText()
        {
            var fontFamily = new FontCollection().AddSystemFonts().Families.First();
            Text text = new Text(fontFamily)
                .WithPoint(new Point(350, 50))
                .WithBrush(Color.Red)
                .WithContent("HELLO");

            canvas.Layers[0].WithDrawable(text).Render().Save(Path.Join(currentPath, "text.png"));
        }
    }
}