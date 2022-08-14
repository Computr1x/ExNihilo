using SixLabors.ImageSharp;

namespace ExNihilo.Rnd;

/// <summary>
/// Stores image generation results.
/// </summary>
public class ImageResult
{
    /// <summary>
    /// The seed with which the image was generated.
    /// </summary>
    public int Seed { get; }
    /// <summary>
    /// Generated image.
    /// </summary>
    public Image Image { get; }
    /// <summary>
    /// Generated captcha text.
    /// </summary>
    public string[] CaptchaText { get; }

    /// <summary>
    /// <inheritdoc cref="ImageResult"/>
    /// </summary>
    public ImageResult(int seed, Image image, string[] captchaText)
    {
        Seed = seed;
        Image = image;
        CaptchaText = captchaText;
    }

    ~ImageResult()
    {
        Image?.Dispose();
    }

    public string GetName(char separator = '_')
    {
        return Seed.ToString() + separator + string.Join(separator, CaptchaText);
    }
}