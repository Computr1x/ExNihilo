using ExNihilo.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExNihilo.Utils;

/// <summary>
/// Allows you to automate randomization and generation of captchas.
/// </summary>
public class CaptchaGenerator : ImageGenerator
{
    private Dictionary<int, string[]> _captchaText = new();

    /// <summary>
    /// <inheritdoc cref="CaptchaGenerator"/>
    /// </summary>
    public CaptchaGenerator(Container template) : base(template)
    {
    }

    /// <summary>  Set manual input for captchas. </summary>
    public CaptchaGenerator WithCaptchaInput(string[] input, int index = 0)
    {
        _captchaText[index] = input;
        return this;
    }

    /// <summary>
    /// Set template for generator.
    /// </summary>
    public override CaptchaGenerator WithTemplate(Container template)
    {
        _template = template;
        return this;
    }
    /// <summary>
    /// Set seeds for randomization.
    /// </summary>
    public override CaptchaGenerator WithSeeds(int[] seeds)
    {
        _seeds = seeds;
        return this;
    }

    /// <summary>
    /// Set seeds for randomization.
    /// </summary>
    public override CaptchaGenerator WithSeed(int seed)
    {
        _seeds = new int[]
        {
            seed
        };

        return (CaptchaGenerator)base.WithSeed(seed);
    }

    /// <summary>
    /// Set count of seeds for randomization.
    /// </summary>
    public override CaptchaGenerator WithSeedsCount(int count)
    {
        _seeds = Enumerable.Range(0, count).ToArray();
        return this;
    }

    /// <summary>
    /// Generate collection of captchas by defined seed and/or input.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when serial number is outside valid     range</exception>
    public override IEnumerable<CaptchaResult> Generate()
    {
        if (_seeds is null || _seeds.Length == 0)
            WithSeedsCount(_captchaText.Values.FirstOrDefault()?.Length ?? 1);

        ValidateFields();

        return _captchaText.Keys.Count > 0 ? GenerateManualCaptcha() : GenerateRandomizedCaptcha();
    }

    private IEnumerable<CaptchaResult> GenerateRandomizedCaptcha()
    {
        Dictionary<int, List<ICaptcha>> captchaIndexMapping = GetContainerCaptchas(_template);

        for (int seedId = 0; seedId < _seeds!.Length; seedId++)
        {
            int seed = _seeds[seedId];
            _template.Randomize(new Random(seed));

            List<string> captchaStrings = new();
            foreach (int captchaIndex in captchaIndexMapping.Keys)
            {
                foreach (var captchaVisual in captchaIndexMapping[captchaIndex])
                    captchaStrings.Add(captchaVisual.Text);
            }

            yield return new(seed, _template.Render(), captchaStrings.ToArray());
        }
    }

    private IEnumerable<CaptchaResult> GenerateManualCaptcha()
    {
        Dictionary<int, List<ICaptcha>> captchaIndexMapping = GetContainerCaptchas(_template);
        int seed;
        List<string> captchaStrings = new();

        for (int seedID = 0; seedID < _seeds!.Length; seedID++)
        {
            seed = _seeds[seedID];
            _template.Randomize(new Random(seed));

            captchaStrings.Clear();

            foreach (int captchaIndex in captchaIndexMapping.Keys.OrderBy(x => x))
            {
                foreach (var captchaVisual in captchaIndexMapping[captchaIndex])
                {
                    if (_captchaText.ContainsKey(captchaIndex))
                        captchaVisual.Text = _captchaText[captchaIndex][seedID];

                    captchaStrings.Add(captchaVisual.Text);
                }
            }

            yield return new(seed, _template.Render(), captchaStrings.ToArray());
        }
    }

    private void ValidateFields()
    {
        int min, max;

        min = _captchaText.Values.Min(x => (int?)x.Length) ?? 0;
        max = _captchaText.Values.Max(x => (int?)x.Length) ?? 0;

        if (min != max)
            throw new ArgumentException("Captcha inputs should have same input array size. ");

        if (max > 0 && _seeds?.Length != max)
            throw new ArgumentException("Seed length must be equal captcha input array size");

        if (min == 0 && _seeds?.Length == 0)
            throw new ArgumentException("For captcha generation you should specify captha input or seeds");
    }

    protected static Dictionary<int, List<ICaptcha>> GetContainerCaptchas(Container container)
    {
        void CreateOrAdd(Dictionary<int, List<ICaptcha>> dic, ICaptcha captcha)
        {
            if (dic.ContainsKey(captcha.Index))
                dic[captcha.Index].Add(captcha);
            else
                dic[captcha.Index] = 
                    new List<ICaptcha>() {
                        captcha
                    };
        }
        Dictionary<int, List<ICaptcha>> captchas = new();

        foreach (var visual in container.Children)
        {
            if (visual is Container innerContainer)
            {
                var innerCaptchas = GetContainerCaptchas(innerContainer);
                foreach(var innerCaptcha in innerCaptchas.Values)
                    innerCaptcha.ForEach(x => CreateOrAdd(captchas, x));
            }
            if (visual is ICaptcha captcha)
            {
                CreateOrAdd(captchas, captcha);
            }
        }

        return captchas;
    }
}
