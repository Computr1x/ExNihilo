using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExNihilo.Utils;

/// <summary>
/// Stores captcha generation results.
/// </summary>
public class CaptchaResult : ImageResult
{
    /// <summary>
    /// Generated captcha text.
    /// </summary>
    public string[] CaptchaText { get; }
    /// <summary>
    /// <inheritdoc cref="CaptchaResult"/>
    /// </summary>
    public CaptchaResult(int seed, Image image, string[] captchaText) : base(seed, image)
    {
        CaptchaText = captchaText;
    }

    public override string GetName()
    {
        return Seed.ToString() + '_' + string.Join('_', CaptchaText);
    }
}
