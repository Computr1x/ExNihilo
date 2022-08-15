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
        Size containerSize = new(512, 256);
        Point center = new(256, 128);
        Container container;
        string currentPath = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "DrawablesTest")).FullName;

        [TestInitialize]
        public void InitContainer()
        {
            container = new Container(containerSize).WithBackground(Color.Orange);
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

            container.WithChild(ellipse).Render().Save(Path.Join(currentPath, "ellipse.png"));
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

            container.WithChild(line).Render().Save(Path.Join(currentPath, "line.png"));
        }

        [TestMethod]
        public void TestPattern()
        {
            Pattern pattern =
                new Pattern()
                    .WithTemplate(new bool[,] { { true, false, false, false }, { false, false, false, false }, { false, false, false, false } })
                    .WithSize(containerSize)
                    .WithBackgroundColor(Color.Transparent)
                    .WithForegroundColor(Color.Gray);
            container.WithChild(pattern).Render();
        }

        [TestMethod]
        public void TestPicture()
        {
            Picture picture = new(@"./Assets/Images/cat.png");
            container.WithChild(picture).Render().Save(Path.Join(currentPath, "picture.png"));
        }

        [TestMethod]
        public void TestPolygon()
        {
            Polygon polygon =
                new Polygon()
                    .WithPoints(new PointF[] { new(400, 200), new(350, 150), new(300, 179) })
                    .WithBrush(BrushType.Solid, Color.Peru)
                    .WithType(DrawableType.Filled);
            container.WithChild(polygon).Render().Save(Path.Join(currentPath, "polygon.png"));
        }

        [TestMethod]
        public void TestRectangle()
        {
            Drawables.Rectangle rectangle =
                new Drawables.Rectangle(50, 200, 100, 50)
                .WithPen(PenType.Dash, 1, Color.Olive)
                .WithType(DrawableType.Outlined);
            container.WithChild(rectangle).Render().Save(Path.Join(currentPath, "rectangle.png"));
        }

        [TestMethod]
        public void TestText()
        {
            var fontFamily = new FontCollection().AddSystemFonts().Families.First();
            Text text = new Text(fontFamily)
                .WithPoint(new Point(350, 50))
                .WithBrush(Color.Red)
                .WithContent("HELLO");

            container.WithChild(text).Render().Save(Path.Join(currentPath, "text.png"));
        }
    }
}