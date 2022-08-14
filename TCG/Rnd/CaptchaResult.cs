using SixLabors.ImageSharp;

namespace TCG.Rnd;

/// <summary>
/// Stores captcha generation results.
/// </summary>
public class CaptchaResult
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
    /// <inheritdoc cref="CaptchaResult"/>
    /// </summary>
    public CaptchaResult(int seed, Image image, string[] captchaText)
    {
        Seed = seed;
        Image = image;
        CaptchaText = captchaText;
    }

    ~CaptchaResult()
    {
        Image?.Dispose();
    }

    public string GetName(char separator = '_')
    {
        return Seed.ToString() + separator + string.Join(separator, CaptchaText);
    }
}