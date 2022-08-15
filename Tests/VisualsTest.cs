using ExNihilo.Base;
using ExNihilo.Visuals;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using Rectangle = ExNihilo.Visuals.Rectangle;

namespace ExNihilo.Tests
{
    [TestClass]
    public class VisualsTest
    {
        Size containerSize = new(512, 256);
        Point center = new(256, 128);
        Container container;
        string currentPath = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "VisualsTest")).FullName;

        [TestInitialize]
        public void InitContainer()
        {
            container = new Container(containerSize).WithBackground(Color.Orange);
        }

        [TestMethod]
        public void TestEllipse()
        {
            var ellipse =
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
                    .WithType(VisualType.FillWithOutline);

            container
                .WithChild(ellipse)
                .Render()
                .Save(Path.Join(currentPath, "ellipse.png"));
        }

        [TestMethod]
        public void TestLine()
        {
            var line =
                new Line()
                    .WithPoints(new PointF[] { new PointF(0, 0), new PointF(512, 256) })
                    .WithPen(pen =>
                    {
                        pen.WithColor(Color.Blue);
                        pen.WithWidth(3);
                        pen.WithType(PenType.Dash);
                    });

            container
                .WithChild(line)
                .Render()
                .Save(Path.Join(currentPath, "line.png"));
        }

        [TestMethod]
        public void TestPattern()
        {
            var pattern =
                new Pattern()
                    .WithTemplate(new bool[,] {
                        { true, false, false, false },
                        { false, false, false, false },
                        { false, false, false, false }
                    })
                    .WithSize(containerSize)
                    .WithBackgroundColor(Color.Transparent)
                    .WithForegroundColor(Color.Gray);
            
            container
                .WithChild(pattern)
                .Render()
                .Save(Path.Join(currentPath, "pattern.png"));
        }

        [TestMethod]
        public void TestPicture()
        {
            container
                .WithChild(new Picture(@"./Assets/Images/cat.png"))
                .Render()
                .Save(Path.Join(currentPath, "picture.png"));
        }

        [TestMethod]
        public void TestPolygon()
        {
            var polygon =
                new Polygon()
                    .WithPoints(new PointF[] { new(400, 200), new(350, 150), new(300, 179) })
                    .WithBrush(BrushType.Solid, Color.Peru)
                    .WithType(VisualType.Filled);

            container
                .WithChild(polygon)
                .Render()
                .Save(Path.Join(currentPath, "polygon.png"));
        }

        [TestMethod]
        public void TestRectangle()
        {
            var rectangle =
                new Rectangle(50, 200, 100, 50)
                .WithPen(PenType.Dash, 1, Color.Olive)
                .WithType(VisualType.Outlined);

            container
                .WithChild(rectangle)
                .Render()
                .Save(Path.Join(currentPath, "rectangle.png"));
        }

        [TestMethod]
        public void TestText()
        {
            var text = new Text(new FontCollection().AddSystemFonts().Families.First())
                .WithPoint(new Point(350, 50))
                .WithBrush(Color.Red)
                .WithContent("HELLO");

            container
                .WithChild(text)
                .Render()
                .Save(Path.Join(currentPath, "text.png"));
        }
    }
}