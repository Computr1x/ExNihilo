using TCG.Base.Hierarchy;
using TCG.Base.Interfaces;

namespace TCG.Rnd;

public class CaptchaGenerator
{
    RandomManager rnd = new RandomManager(0);

    private Canvas _template;
    private int[] _seeds;
    private Dictionary<int, string[]> _captchaText = new Dictionary<int, string[]>();

    public CaptchaGenerator(Canvas template)
    {
        _template = template;
    }

    public CaptchaGenerator WithTemplate(Canvas template)
    {
        _template = template;
        return this;
    }

    public CaptchaGenerator WithSeeds(int[] seeds)
    {
        _seeds = seeds;
        return this;
    }

    public CaptchaGenerator WithSeedsCount(int count)
    {
        _seeds = Enumerable.Range(0, count).ToArray();
        return this;
    }

    public CaptchaGenerator WithCaptchaInput(string[] input, int index = 0)
    {
        _captchaText[index] = input;
        return this;
    }

    public IEnumerable<CaptchaResult> Generate()
    {
        if (_seeds == null || _seeds.Length == 0)
        {
            int count = _captchaText.Values.FirstOrDefault()?.Length ?? 0;
            WithSeedsCount(count);
        }

        ValidateFields();

        return _captchaText.Keys.Count > 0 ? GenerateManualCaptcha() : GenerateRandomizedCaptcha();
    }

    private IEnumerable<CaptchaResult> GenerateRandomizedCaptcha()
    {
        Dictionary<int, List<ICaptcha>> captchaIndexMapping = GetCanvasCaptchas(_template);

        for (int seedId = 0; seedId < _seeds.Length; seedId++)
        {
            int seed = _seeds[seedId];
            rnd.ResetRandom(seed);
            rnd.RandomizeCanvas(_template);

            List<string> captchaStrings = new List<string>();
            foreach (int captchaIndex in captchaIndexMapping.Keys)
            {
                foreach (var captchaDrawable in captchaIndexMapping[captchaIndex])
                {
                    captchaStrings.Add(captchaDrawable.Text);

                }
            }
            yield return new(seed, _template.Render(), captchaStrings.ToArray());
        }
    }

    private IEnumerable<CaptchaResult> GenerateManualCaptcha()
    {
        Dictionary<int, List<ICaptcha>> captchaIndexMapping = GetCanvasCaptchas(_template);
        

        for (int seedId = 0; seedId < _seeds.Length; seedId++)
        {
            int seed = _seeds[seedId];
            rnd.ResetRandom(seed);
            rnd.RandomizeCanvas(_template);

            List<string> captchaStrings = new List<string>();
            foreach (int captchaIndex in captchaIndexMapping.Keys.OrderBy(x => x))
            {
                foreach (var captchaDrawable in captchaIndexMapping[captchaIndex])
                {
                    if (_captchaText.ContainsKey(captchaIndex))
                        captchaDrawable.Text = _captchaText[captchaIndex][seedId];
                    captchaStrings.Add(captchaDrawable.Text);
                }
                    
            }
            yield return new(seed, _template.Render(), captchaStrings.ToArray());
        }
    }

    private void ValidateFields()
    {
        int min, max;

        min = _captchaText.Values.Min(x => ((int?)x.Length)) ?? 0;
        max = _captchaText.Values.Max(x => ((int?)x.Length)) ?? 0;

        if (min != max)
            throw new ArgumentException("Captcha inputs should have same input array size. ");

        if (max > 0 && _seeds?.Length != max)
            throw new ArgumentException("Seed length must be equal captcha input array size");

        if (min == 0 && _seeds?.Length == 0)
            throw new ArgumentException("For captcha generation you should specify captha input or seeds");
    }

    protected Dictionary<int, List<ICaptcha>> GetCanvasCaptchas(Canvas canvas)
    {
        Dictionary<int, List<ICaptcha>> captchas = new();
        foreach (var layer in canvas.Layers)
        {
            foreach (var drawable in layer.Drawables)
            {
                if (drawable is ICaptcha captcha)
                {
                    if(captchas.ContainsKey(captcha.Index))
                        captchas[captcha.Index].Add(captcha);
                    else
                        captchas[captcha.Index] = new List<ICaptcha>() { captcha };
                }
            }
        }
        return captchas;
    }
}