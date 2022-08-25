namespace ExNihilo.Tests;

[TestClass]
public class VisualsTest : Test
{
    Size _containerSize = new(512, 256);
    Point _center = new(256, 128);
    Container _container;

    public VisualsTest()
    {
        _container = new Container(_containerSize)
            .WithBackground(Color.Orange);
    }

    [TestMethod]
    public void TestEllipse()
    {
        var ellipse =
            new Ellipse()
                .WithPoint(_center)
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
                .WithType(VisualType.Filled | VisualType.Outlined);

        _container
            .WithChild(ellipse)
            .Render()
            .Save(Path.Join(CurrentPath, "ellipse.png"));
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

        _container
            .WithChild(line)
            .Render()
            .Save(Path.Join(CurrentPath, "line.png"));
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
                .WithSize(_containerSize)
                .WithBackgroundColor(Color.Transparent)
                .WithForegroundColor(Color.Gray);
        
        _container
            .WithChild(pattern)
            .Render()
            .Save(Path.Join(CurrentPath, "pattern.png"));
    }

    [TestMethod]
    public void TestPicture()
    {
        _container
            .WithChild(new Picture(@"./Assets/Images/cat.png"))
            .Render()
            .Save(Path.Join(CurrentPath, "picture.png"));
    }

    [TestMethod]
    public void TestPolygon()
    {
        var polygon =
            new Polygon()
                .WithPoints(new PointF[] { new(400, 200), new(350, 150), new(300, 179) })
                .WithBrush(BrushType.Solid, Color.Peru)
                .WithType(VisualType.Filled);

        _container
            .WithChild(polygon)
            .Render()
            .Save(Path.Join(CurrentPath, "polygon.png"));
    }

    [TestMethod]
    public void TestRectangle()
    {
        var rectangle =
            new Rectangle(50, 200, 100, 50)
            .WithPen(PenType.Dash, 1, Color.Olive)
            .WithType(VisualType.Outlined);

        _container
            .WithChild(rectangle)
            .Render()
            .Save(Path.Join(CurrentPath, "rectangle.png"));
    }

    [TestMethod]
    public void TestText()
    {
        var fontFamily = new FontCollection().Add(@".\Assets\Fonts\OpenSans.ttf");
        var text = new Text(fontFamily)
            .WithPoint(new Point(350, 50))
            .WithBrush(Color.Red)
            .WithContent("HELLO");

        _container
            .WithChild(text)
            .Render()
            .Save(Path.Join(CurrentPath, "text.png"));
    }
}
