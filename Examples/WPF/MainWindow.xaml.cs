using ExNihilo.Base;
using ExNihilo.Rnd;
using ExNihilo.Visuals;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF;

public class CaptchaViewer : FrameworkElement {
	public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
		nameof(ImageSource),
		typeof(ImageSource),
        typeof(CaptchaViewer),
		new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender)
	);

	public ImageSource ImageSource
	{
		get => (ImageSource) GetValue(ImageSourceProperty);
		set => SetValue(ImageSourceProperty, value);
	}

	public void Randomize()
	{
        var renderSize = RenderSize;

        if (renderSize.Width == 0 || renderSize.Height == 0)
            return;

        SixLabors.ImageSharp.Size visualSize = new((int) renderSize.Width, (int) renderSize.Height);
        SixLabors.ImageSharp.Point textCenter = new(visualSize.Width / 2, visualSize.Height / 2);

        var visual = new Container(visualSize)
            .WithBackground(SixLabors.ImageSharp.Color.Orange)
            .WithContainer(
                new Container(visualSize)
                    .WithChildren(
                        Enumerable.Range(0, 15).Select(
                            x => new ExNihilo.Visuals.Ellipse()
                                .WithRandomizedPoint(0, visualSize.Width, 0, visualSize.Height)
                                .WithRandomizedSize(30, 60)
                                .WithBrush(brush => {
                                    brush.WithRandomizedColor(50);
                                    brush.WithRandomizedType();
                                })
                                .WithType(VisualType.Filled)
                        )
                    )
                    .WithBlendPercentage(0.5f))
            .WithChild(
                new Captcha(new FontCollection().AddSystemFonts().Families.First())
                    .WithContent("Hello world")
                    .WithPoint(textCenter)
                    .WithFontSize(60)
                    .WithRandomizedBrush(50)
                    .WithType(VisualType.Filled)
                );

        MemoryStream memoryStream = new();
        visual.Randomize(new Random());
        visual.Render().Save(memoryStream, new PngEncoder());
        memoryStream.Seek(0, SeekOrigin.Begin);

        ImageSource = BitmapFrame.Create(memoryStream, BitmapCreateOptions.None, BitmapCacheOption.None);
    }

	protected override void OnRender(DrawingContext dc)
	{
        Rect rect = new(RenderSize);

        // Transparent background for hit testing
        dc.DrawRectangle(Brushes.Transparent, null, rect);

        // Captcha image
        if (ImageSource is not null)
            dc.DrawImage(ImageSource, new Rect(RenderSize));
	}
}

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();

        // First generation
        CaptchaViewer.Loaded += (s, e) => CaptchaViewer.Randomize();
        CaptchaViewer.MouseDown += (s, e) => CaptchaViewer.Randomize();
    }
}
