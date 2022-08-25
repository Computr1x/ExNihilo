namespace ExNihilo.Tests;

[TestClass]
public class SaverTest : Test
{
	private static Container CreateTemplate()
	{
		Size containerSize = new(512, 256);
		Point center = new(256, 128);

        var fontFamily = new FontCollection().Add(@".\Assets\Fonts\OpenSans.ttf");

        return new Container(containerSize)
			.WithBackground(Color.Orange)
			.WithContainer(
				new Container(containerSize)
					.WithChildren(
						Enumerable.Range(0, 15).Select(
							x => new Ellipse()
								.WithRandomizedPoint(0, 512, 0, 256)
								.WithRandomizedSize(30, 60)
								.WithBrush(brush =>
								{
									brush.WithRandomizedColor(50);
									brush.WithRandomizedType();
								})
								.WithType(VisualType.Filled)
                         )
                    )
					.WithBlendPercentage(0.5f)
			)
			.WithContainer(
				new Container(containerSize)
					.WithChild(
						new Captcha(fontFamily)
							.WithRandomizedContent(content => content.WithLength(6))
							.WithPoint(center)
							.WithFontSize(100)
							.WithRandomizedBrush(50)
							.WithType(VisualType.Filled)
                   )
			);
	}

	[TestMethod]
	public void TestSaveAsSeparateFiles()
	{
		var captchaResults = new ImageGenerator(CreateTemplate()).WithSeedsCount(5).Generate();

		new ImageSaver(captchaResults).WithOutputPath(CurrentPath).CreateFolder("TestFolder1").WithFilePrefix("_test_").WithOutputType(ImageType.Png).Save();
		new ImageSaver(captchaResults).WithOutputPath(Path.Join(CurrentPath, "TestFolder1")).WithFilePrefix("_test_").WithOutputType(ImageType.Jpeg).Save();
	}

	[TestMethod]
	public void TestSaveAsZip()
	{
		var captchaResults = new ImageGenerator(CreateTemplate()).WithSeedsCount(5).Generate();

		new ImageSaver(captchaResults).WithOutputPath(CurrentPath).SaveAsZip();
		new ImageSaver(captchaResults).WithOutputPath(CurrentPath).WithFilePrefix("_test_").WithOutputType(ImageType.Jpeg).SaveAsZip("archive2");
	}
}
