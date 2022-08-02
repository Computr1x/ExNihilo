using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Hierarchy;

namespace TCG.Rnd.Managers;

public class CaptchaGenerator
{
    RandomManager rnd = new RandomManager(0);

    public IEnumerable<Image> Generate(Canvas template, int[] seeds) 
    {
        foreach (int seed in seeds)
        {
            rnd.ResetRandom(seed);
            rnd.RandomizeCanvas(template);
            yield return template.Render();
        }
    }

    public IEnumerable<Image> Generate(Canvas template, int[] seeds, string[] captchaText, Action<Canvas, string> setCaptchaText) 
    {
        if (seeds.Length != captchaText.Length)
            throw new ArgumentException("Seed length must be equal captchaText length");

        foreach(var (seed, text) in Enumerable.Zip(seeds, captchaText))
        {
            rnd.ResetRandom(seed);
            rnd.RandomizeCanvas(template);
            setCaptchaText(template, text);
            yield return template.Render();
        }
    }

    public IEnumerable<Image> Generate(Canvas template, int[] seeds, string[][] captchaText, Action<Canvas, string[]> setCaptchaText)
    {
        if (seeds.Length != captchaText.Length)
            throw new ArgumentException("Seed length must be equal captchaText length");

        foreach (var (seed, textArray) in Enumerable.Zip(seeds, captchaText))
        {
            rnd.ResetRandom(seed);
            rnd.RandomizeCanvas(template);
            setCaptchaText(template, textArray);
            yield return template.Render();
        }
    }
}

public class TestGenerator : ISetTemplateStage, ISeedSetStage, ISetCaptchaSetTextActionStage, ISetCaptchaSetMultipleTextActionStage, IGenerateBySeedAndTextStage, IGenerateBySeedAndMultipleTextStage, IGenerateBySeedStage
{
    RandomManager rnd = new RandomManager(0);

    private Canvas _template;
    private int[] _seeds;
    private string[] _captchaText;
    private string[][] _captchaMultipleText;
    private Action<Canvas, string[]> _setCaptchaMultipleTextAction;
    private Action<Canvas, string> _setCaptchaTextAction;

    private TestGenerator() { }

    public static ISetTemplateStage CreateGenerator()
    {
        return new TestGenerator();
    }

    ISeedSetStage ISetTemplateStage.SetTemplate(Canvas template)
    {
        _template = template;
        return this;
    }

    public IGenerateBySeedStage SetSeeds(int[] seeds)
    {
        _seeds = seeds;
        return this;
    }

    IEnumerable<Image> IGenerateBySeedStage.Generate()
    {
        foreach (int seed in _seeds)
        {
            rnd.ResetRandom(seed);
            rnd.RandomizeCanvas(_template);
            yield return _template.Render();
        }
    }

    IEnumerable<Image> IGenerateBySeedAndTextStage.Generate()
    {
        foreach (var (seed, text) in Enumerable.Zip(_seeds, _captchaText))
        {
            rnd.ResetRandom(seed);
            rnd.RandomizeCanvas(_template);
            _setCaptchaTextAction(_template, text);
            yield return _template.Render();
        }
    }

    IEnumerable<Image> IGenerateBySeedAndMultipleTextStage.Generate()
    {
        foreach (var (seed, textArray) in Enumerable.Zip(_seeds, _captchaMultipleText))
        {
            rnd.ResetRandom(seed);
            rnd.RandomizeCanvas(_template);
            _setCaptchaMultipleTextAction(_template, textArray);
            yield return _template.Render();
        }
    }

    ISetCaptchaSetTextActionStage IGenerateBySeedStage.SetCaptchaTexts(string[] captchaTexts)
    {
        if (_seeds.Length != captchaTexts.Length)
            throw new ArgumentException("Seed length must be equal captchaText length");
        _captchaText = captchaTexts;
        return this;
    }

    ISetCaptchaSetMultipleTextActionStage IGenerateBySeedStage.SetCaptchaTexts(string[][] captchaTexts)
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
}



public interface ISetTemplateStage
{
    public ISeedSetStage SetTemplate(Canvas template);
}

public interface ISeedSetStage
{
    public IGenerateBySeedStage SetSeeds(int[] seeds);
}

public interface ISetCaptchaSetTextActionStage
{
    public IGenerateBySeedAndTextStage SetCaptchaSetTextAction(Action<Canvas, string> setCaptchaTextAction);
}

public interface ISetCaptchaSetMultipleTextActionStage
{
    public IGenerateBySeedAndMultipleTextStage SetCaptchaSetTextAction(Action<Canvas, string[]> setCaptchaText);
}

public interface IGenerateBySeedAndTextStage
{
    public IEnumerable<Image> Generate();
}

public interface IGenerateBySeedAndMultipleTextStage
{
    public IEnumerable<Image> Generate();
}

public interface IGenerateBySeedStage
{
    public IEnumerable<Image> Generate();
    public ISetCaptchaSetTextActionStage SetCaptchaTexts(string[] captchaTexts);
    public ISetCaptchaSetMultipleTextActionStage SetCaptchaTexts(string[][] captchaTexts);
}

public class CaptchaResult
{
    public int Seed { get; }
    public Image Image { get; }


}