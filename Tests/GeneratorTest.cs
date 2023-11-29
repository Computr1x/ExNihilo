namespace ExNihilo.Tests;

[TestClass]
public class GeneratorTest : Test
{
	private static Container CreateTemplate()
	{
		Size containerSize = new(512, 256);
		Point center = new(256, 128);

        var fontFamily = new FontCollection().Add(Path.GetFullPath(@"./Assets/Fonts/OpenSans.ttf"));

        return new Container(containerSize)
			.WithBackground(Color.Orange)
			.WithContainer(
				new Container(containerSize)
					.WithChildren(
						Enumerable.Range(0, 15).Select(
                            x => new Ellipse()
	                            .WithRandomizedPoint(0, 512, 0, 256)
	                            .WithRandomizedSize(30, 60)
	                            .WithBrush(brush => {
		                            brush.WithRandomizedColor(50);
		                            brush.WithRandomizedType();
	                            })
	                            .WithType(VisualType.Filled)
                        )
                    )
					.WithBlendPercentage(0.5f))
			.WithContainer(
				new Container(containerSize)
				.WithChild(
					new Captcha(fontFamily)
						.WithRandomizedContent(content => content.WithLength(6))
						.WithPoint(center)
						.WithFontSize(100)
						.WithRandomizedBrush(50)
						.WithType(VisualType.Filled)));
	}

	[TestMethod]
	public void TestGeneratorWithSeeds()
	{
		foreach(var captchaRes in new CaptchaGenerator(CreateTemplate()).WithSeedsCount(5).Generate())
		{
			captchaRes.Image.SaveAsPng(Path.Combine(CurrentPath, captchaRes.GetName() + ".png"));
		}
	}

	[TestMethod]
	public void TestGeneratorWithText()
	{
		foreach (var captchaRes in new CaptchaGenerator(CreateTemplate()).WithCaptchaInput(new string[] { "Igor", "so", "small"}).Generate())
		{
			captchaRes.Image.SaveAsPng(Path.Combine(CurrentPath, captchaRes.GetName() + ".png"));
		}
	}

	[TestMethod]
	public void TestGeneratorWithoutProperties()
	{
		try
		{
			foreach (var captchaRes in new ImageGenerator(CreateTemplate()).Generate())
			{
				captchaRes.Image.SaveAsPng(Path.Combine(CurrentPath, captchaRes.GetName() + ".png"));
			}
		}
		catch (Exception e)
		{
			Assert.IsInstanceOfType(e, typeof(ArgumentException));
		}
	}

	private static Container CreateSecondTemplate()
	{
		Size containerSize = new(512, 256);
		Point center = new(0, 128);

        var fontFamily = new FontCollection().Add(Path.GetFullPath(@"./Assets/Fonts/OpenSans.ttf"));

        return new Container(containerSize)
			.WithBackground(Color.Orange)
			.WithContainer(
				new Container(containerSize)
					.WithChildren(
						Enumerable.Range(0, 15).Select(
							x => new Ellipse()
								.WithRandomizedPoint(0, 512, 0, 256)
								.WithRandomizedSize(30, 60)
								.WithBrush(brush => {
									brush.WithRandomizedColor(50);
									brush.WithRandomizedType();
								})
								.WithType(VisualType.Filled)))
					.WithBlendPercentage(0.5f))
			.WithContainer(
				new Container(containerSize)
					.WithChild(
						new CaptchaSymbols(fontFamily)
							.WithRandomizedContent(content => {
								content.WithLength(6);
							})
							.WithPoint(center)
							.WithFontSize(100)
							.WithRandomizedBrush(50)
							.WithSymbolsEffect(
								new Rotate().WithRandomizedDegree(-30, 30))
							));
	}

	[TestMethod]
	public void TestGeneratorWithCaptchaSymbols()
	{
		foreach (var captchaRes in new CaptchaGenerator(CreateSecondTemplate()).WithSeedsCount(5).Generate())
		{
			captchaRes.Image.SaveAsPng(Path.Combine(CurrentPath, "s_" + captchaRes.GetName() + ".png"));
		}
	}

	private static Container CreateThirdTemplate()
	{
		Size containerSize = new(512, 256);
		Point center = new(256, 128);

        var fontFamily = new FontCollection().Add(Path.GetFullPath(@"./Assets/Fonts/OpenSans.ttf"));

        return new Container(containerSize)
			.WithBackground(Color.Orange)
			.WithContainer(
				new Container(containerSize)
				.WithChild(
					new Captcha(fontFamily)
						.WithIndex(0)
						.WithRandomizedContent(content =>
						{
							content.WithLength(6);
						})
						.WithPoint(center)
						.WithFontSize(100)
						.WithRandomizedBrush(50)
						)
				.WithChild(
				    new Captcha(fontFamily)
				        .WithIndex(1)
				        .WithPoint(center)
				        .WithFontSize(100)
				        .WithRandomizedBrush(50)
				)
			);
	}

	[TestMethod]
	public void TestGeneratorWithRandomAndText()
	{
		foreach (var captchaRes in new CaptchaGenerator(CreateThirdTemplate()).WithCaptchaInput(new string[] { "abc", "def", "xyz"}, 1).Generate())
		{
			captchaRes.Image.SaveAsPng(Path.Combine(CurrentPath, "s2_" + captchaRes.GetName() + ".png"));
		}
	}
}
