using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;
using ExNihilo.Extensions.Processors;
using ExNihilo.Base.Parameters;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of slices effect on an <see cref="IDrawable"/>
/// </summary>
public class Slices : IEffect
{
    /// <summary>
    /// Seed value
    /// </summary>
    public IntParameter Seed { get; set; } = new(0) { Min = 0, Max = int.MaxValue };
    /// <summary>
    /// Count of slices. Must greater or equal to 1.
    /// </summary>
    public IntParameter Count { get; set; } = new(10) { Min = 1, Max = 10 };
    /// <summary>
    /// Min value of slice shift.
    /// </summary>
    public IntParameter MinOffset { get; set; } = new() { Min = -10, Max = 0 };
    /// <summary>
    /// Max value of slice shift
    /// </summary>
    public IntParameter MaxOffset { get; set; } = new() { Min = 0, Max = 10 };
    /// <summary>
    /// Height of the slice. Must greater or equal to 1.
    /// </summary>
    public IntParameter SliceHeight { get; set; } = new(1, int.MaxValue, 1) { Min = 1, Max = 10 };

    /// <summary>
    /// <inheritdoc cref="Slices"/>
    /// </summary>
    public Slices() { }

    /// <summary>
    /// <inheritdoc cref="Slices"/>
    /// </summary>
    /// <param name="count"><inheritdoc cref="Count" path="/summary"/></param>
    /// <param name="sliceHeight"><inheritdoc cref="SliceHeight" path="/summary"/></param>
    public Slices(int count, int sliceHeight)
    {
        Count.Value = count;
        SliceHeight.Value = sliceHeight;
    }

    /// <summary>
    /// Set Count value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Count" path="/summary"/></param>
    public Slices WithCount(int value)
    {
        Count.Value = value;
        return this;
    }

    /// <summary>
    /// Set slices Count randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Count" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Count" path="/summary"/></param>
    public Slices WithRandomizedCount(int min, int max)
    {
        Count.Min = min;
        Count.Max = max;
        return this;
    }

    /// <summary>
    /// Set Seed value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Seed" path="/summary"/></param>
    public Slices WithSeed(int value)
    {
        Seed.Value = value;
        return this;
    }
    /// <summary>
    /// Set Seed randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Seed" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Seed" path="/summary"/></param>
    public Slices WithRandomizedSeed(int min, int max)
    {
        Seed.Min = min;
        Seed.Max = max;
        return this;
    }

    /// <summary>
    /// Set MinOffset value
    /// </summary>
    /// <param name="value"><inheritdoc cref="MinOffset" path="/summary"/></param>
    public Slices WithMinOffset(int value)
    {
        MinOffset.Value = value;
        return this;
    }
    /// <summary>
    /// Set Min offset randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="MinOffset" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="MinOffset" path="/summary"/></param>
    public Slices WithRandomizedMinOffset(int min, int max)
    {
        MinOffset.Min = min;
        MinOffset.Max = max;
        return this;
    }
    /// <summary>
    /// Set MaxOffset value
    /// </summary>
    /// <param name="value"><inheritdoc cref="MaxOffset" path="/summary"/></param>
    public Slices WithMaxOffset(int value)
    {
        MaxOffset.Value = value;
        return this;
    }
    /// <summary>
    /// Set Max offset randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="MaxOffset" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="MaxOffset" path="/summary"/></param>
    public Slices WithRandomizedMaxOffset(int min, int max)
    {
        MaxOffset.Min = min;
        MaxOffset.Max = max;
        return this;
    }
    /// <summary>
    /// Set Slide height value
    /// </summary>
    /// <param name="value"><inheritdoc cref="SliceHeight" path="/summary"/></param>
    public Slices WithSliceHeight(int value)
    {
        SliceHeight.Value = value;
        return this;
    }
    /// <summary>
    /// Set slice Height randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="SliceHeight" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="SliceHeight" path="/summary"/></param>
    public Slices WithRandomizedSliceHeight(int min, int max)
    {
        SliceHeight.Min = min;
        SliceHeight.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Slices(Seed, Count, SliceHeight, MinOffset, MaxOffset));
}
