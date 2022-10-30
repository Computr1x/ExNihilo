using SixLabors.ImageSharp;
using System.Data.Common;

namespace ExNihilo.Utils;

/// <summary>
/// Stores image generation results.
/// </summary>
public class ImageResult : IDisposable
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
    /// <inheritdoc cref="ImageResult"/>
    /// </summary>
    public ImageResult(int seed, Image image)
    {
        Seed = seed;
        Image = image;
    }

    public virtual string GetName()
    {
        return Seed.ToString();
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Image?.Dispose();
        }
    }
}