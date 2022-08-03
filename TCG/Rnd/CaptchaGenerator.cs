using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Hierarchy;

namespace TCG.Rnd;

public class CaptchaGenerator : ISetTemplate, IHasTemplate, IHasSeedAndTemplateAndCaptchaTexts, IHasSeedAndTemplateAndGetTextFunc,
    IHasSeedAndTemplateAndCaptchMultipleTexts, IGenerateBySeedAndTextStage, IGenerateBySeedAndMultipleTextStage, IHasSeedAndTemplate
{
    RandomManager rnd = new RandomManager(0);

    private Canvas _template;
    private int[] _seeds;
    private string[] _captchaText;
    private string[][] _captchaMultipleText;
    private Func<Canvas, string> _getCaptchaTextFunc;
    private Action<Canvas, string[]> _setCaptchaMultipleTextAction;
    private Action<Canvas, string> _setCaptchaTextAction;

    private CaptchaGenerator() { }

    public static ISetTemplate Create()
    {
        return new CaptchaGenerator();
    }

    IHasTemplate ISetTemplate.SetTemplate(Canvas template)
    {
        _template = template;
        return this;
    }

    public IHasSeedAndTemplate SetSeeds(int[] seeds)
    {
        _seeds = seeds;
        return this;
    }

    IEnumerable<CaptchaResult> IHasSeedAndTemplateAndGetTextFunc.Generate()
    {
        foreach (int seed in _seeds)
        {
            rnd.ResetRandom(seed);
            rnd.RandomizeCanvas(_template);

            string captchaText = _getCaptchaTextFunc(_template);
            yield return new(seed, _template.Render(), new string[] { captchaText });
        }
    }

    IEnumerable<CaptchaResult> IGenerateBySeedAndTextStage.Generate()
    {
        foreach (var (seed, text) in _seeds.Zip(_captchaText))
        {
            rnd.ResetRandom(seed);
            rnd.RandomizeCanvas(_template);
            _setCaptchaTextAction(_template, text);
            Console.WriteLine(text);
            yield return new CaptchaResult(seed, _template.Render(), new string[] { text });
        }
    }

    IEnumerable<CaptchaResult> IGenerateBySeedAndMultipleTextStage.Generate()
    {
        foreach (var (seed, textArray) in _seeds.Zip(_captchaMultipleText))
        {
            rnd.ResetRandom(seed);
            rnd.RandomizeCanvas(_template);
            _setCaptchaMultipleTextAction(_template, textArray);
            yield return new CaptchaResult(seed, _template.Render(), textArray);
        }
    }

    IHasSeedAndTemplateAndCaptchaTexts IHasSeedAndTemplate.SetCaptchaTexts(string[] captchaTexts)
    {
        if (_seeds.Length != captchaTexts.Length)
            throw new ArgumentException("Seed length must be equal captchaText length");
        _captchaText = captchaTexts;
        return this;
    }

    IHasSeedAndTemplateAndCaptchMultipleTexts IHasSeedAndTemplate.SetCaptchaTexts(string[][] captchaTexts)
    {
        if (_seeds.Length != captchaTexts.Length)
            throw new ArgumentException("Seed length must be equal captchaText length");
        _captchaMultipleText = captchaTexts;
        return this;
    }

    public IGenerateBySeedAndTextStage SetCaptchaSetTextAction(Action<Canvas, string> setCaptchaTextAction)
    {
        _setCaptchaTextAction = setCaptchaTextAction;
        return this;
    }

    public IGenerateBySeedAndMultipleTextStage SetCaptchaSetTextAction(Action<Canvas, string[]> setCaptchaText)
    {
        _setCaptchaMultipleTextAction = setCaptchaText;
        return this;
    }

    public IEnumerable<Image> Generate()
    {
        throw new NotImplementedException();
    }

    public IHasSeedAndTemplateAndGetTextFunc SetFunctionGetCaptchaText(Func<string, Canvas> getCaptchaText)
    {
        throw new NotImplementedException();
    }
}

public interface ISetTemplate
{
    public IHasTemplate SetTemplate(Canvas template);
}

public interface IHasTemplate
{
    public IHasSeedAndTemplate SetSeeds(int[] seeds);
}

public interface IHasSeedAndTemplateAndCaptchaTexts
{
    public IGenerateBySeedAndTextStage SetCaptchaSetTextAction(Action<Canvas, string> setCaptchaTextAction);
}

public interface IHasSeedAndTemplateAndCaptchMultipleTexts
{
    public IGenerateBySeedAndMultipleTextStage SetCaptchaSetTextAction(Action<Canvas, string[]> setCaptchaText);
}

public interface IHasSeedAndTemplateAndGetTextFunc
{
    public IEnumerable<CaptchaResult> Generate();
}

public interface IGenerateBySeedAndTextStage
{
    public IEnumerable<CaptchaResult> Generate();
}

public interface IGenerateBySeedAndMultipleTextStage
{
    public IEnumerable<CaptchaResult> Generate();
}

public interface IHasSeedAndTemplate
{
    public IHasSeedAndTemplateAndGetTextFunc SetFunctionGetCaptchaText(Func<string, Canvas> getCaptchaText);
    public IHasSeedAndTemplateAndCaptchaTexts SetCaptchaTexts(string[] captchaTexts);
    public IHasSeedAndTemplateAndCaptchMultipleTexts SetCaptchaTexts(string[][] captchaTexts);
}