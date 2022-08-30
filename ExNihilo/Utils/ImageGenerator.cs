using ExNihilo.Base;

namespace ExNihilo.Utils;

/// <summary>
/// Allows you to automate randomization and generation of images.
/// </summary>
public class ImageGenerator
{
    protected Container _template;
    protected int[]? _seeds;

    /// <summary>
    /// <inheritdoc cref="ImageGenerator"/>
    /// </summary>
    public ImageGenerator(Container template)
    {
        _template = template;
    }

    /// <summary>
    /// Set template for generator.
    /// </summary>
    public virtual ImageGenerator WithTemplate(Container template)
    {
        _template = template;
        return this;
    }
    /// <summary>
    /// Set seeds for randomization.
    /// </summary>
    public virtual ImageGenerator WithSeeds(int[] seeds)
    {
        _seeds = seeds;
        return this;
    }

    /// <summary>
    /// Set seeds for randomization.
    /// </summary>
    public virtual ImageGenerator WithSeed(int seed)
    {
        _seeds = new int[]
        {
            seed
        };

        return this;
    }

    /// <summary>
    /// Set count of seeds for randomization.
    /// </summary>
    public virtual ImageGenerator WithSeedsCount(int count)
    {
        _seeds = Enumerable.Range(0, count).ToArray();
        return this;
    }


    public virtual IEnumerable<ImageResult> Generate()
    {
        ValidateFields();

        for (int seedId = 0; seedId < _seeds!.Length; seedId++)
        {
            int seed = _seeds[seedId];
            _template.Randomize(new Random(seed));

            yield return new(seed, _template.Render());
        }
    }


    public virtual void ValidateFields()
    {
        if (_seeds == null || _seeds.Length == 0)
            throw new ArgumentException("For captcha generation you should specify captha input or seeds");
    }
}