using SixLabors.ImageSharp;

namespace TCG.Rnd;

public class CaptchaResult
{
    public int Seed { get; }
    public Image Image { get; }
    public string[] CaptchaText { get; }

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