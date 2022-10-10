﻿using SixLabors.ImageSharp;
using System.IO.Compression;

namespace ExNihilo.Utils;

/// <summary>
/// Allows you to automate the saving of captcha generation results.
/// </summary>
public class ImageSaver
{
    private string _path = "";
    private string _folderName = "";
    private string _prefix = "";
    private ImageType _imageType = ImageType.Png;
    private IEnumerable<ImageResult> _imageResults;


    /// <summary>
    /// <inheritdoc cref="ImageSaver"/>
    /// </summary>
    public ImageSaver(IEnumerable<ImageResult> imageResults)
    {
        _imageResults = imageResults;
    }
    /// <summary>
    /// <inheritdoc cref="ImageSaver"/>
    /// </summary>
    public ImageSaver(IEnumerable<ImageResult> imageResults, ImageType imageType) : this(imageResults)
    {
        _imageType = imageType;
    }
    /// <summary>
    /// <inheritdoc cref="ImageSaver"/>
    /// </summary>
    public ImageSaver(IEnumerable<ImageResult> imageResults, string path, ImageType imageType) : this(imageResults, imageType)
    {
        _path = path;
    }

    /// <summary>
    /// Save results synchronously as separate files.
    /// </summary>
    public void Save()
    {
        foreach(ImageResult imageResult in _imageResults)
        {
            string resPath = Path.Join(_path, _prefix + imageResult.GetName() + GetImageTypeExtenstion(_imageType));
            switch (_imageType)
            {
                case ImageType.Png:
                    imageResult.Image.SaveAsPng(resPath);
                    break;
                case ImageType.Jpeg:
                    imageResult.Image.SaveAsJpeg(resPath);
                    break;
                case ImageType.Bmp:
                    imageResult.Image.SaveAsBmp(resPath);
                    break;
                case ImageType.Webp:
                    imageResult.Image.SaveAsWebp(resPath);
                    break;
            }
        }
    }

    /// <summary>
    /// Save results asynchronously as separate files.
    /// </summary>
    public async Task SaveAsync()
    {
        foreach (ImageResult captchaResult in _imageResults)
        {
            string resPath = Path.Join(_path, _prefix + captchaResult.GetName() + GetImageTypeExtenstion(_imageType));
            switch (_imageType)
            {
                case ImageType.Png:
                    await captchaResult.Image.SaveAsPngAsync(resPath);
                    break;
                case ImageType.Jpeg:
                    await captchaResult.Image.SaveAsJpegAsync(resPath);
                    break;
                case ImageType.Bmp:
                    await captchaResult.Image.SaveAsBmpAsync(resPath);
                    break;
                case ImageType.Webp:
                    await captchaResult.Image.SaveAsWebpAsync(resPath);
                    break;
            }
        }
    }
    /// <summary>
    /// Save results synchronously as zip archive.
    /// </summary>
    public void SaveAsZip(string archiveName = "archive", CompressionLevel compressionLevel = CompressionLevel.Fastest)
    {
        using var archiveStream = new FileStream(Path.Join(_path, archiveName + ".zip"), FileMode.Create);
        using var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true);
       
        foreach (ImageResult captchaResult in _imageResults)
        {
            var zipArchiveEntry = archive.CreateEntry(captchaResult.GetName() + GetImageTypeExtenstion(_imageType), compressionLevel);

            using var zipStream = zipArchiveEntry.Open();
            switch (_imageType)
            {
                case ImageType.Png:
                    captchaResult.Image.SaveAsPng(zipStream);
                    break;
                case ImageType.Jpeg:
                    captchaResult.Image.SaveAsJpeg(zipStream);
                    break;
                case ImageType.Bmp:
                    captchaResult.Image.SaveAsBmp(zipStream);
                    break;
                case ImageType.Webp:
                    captchaResult.Image.SaveAsWebp(zipStream);
                    break;
            }
        }
    }
    /// <summary>
    /// Save results asynchronously as zip archive.
    /// </summary>
    public async Task SaveAsZipAsync(string archiveName = "archive", CompressionLevel compressionLevel = CompressionLevel.Fastest)
    {
        using var archiveStream = new FileStream(Path.Join(_path, archiveName + ".zip"), FileMode.CreateNew, FileAccess.Write);
        using var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true);
        foreach (ImageResult captchaResult in _imageResults)
        {
            var zipArchiveEntry = archive.CreateEntry(captchaResult.GetName() + GetImageTypeExtenstion(_imageType), compressionLevel);

            using var zipStream = zipArchiveEntry.Open();
            switch (_imageType)
            {
                case ImageType.Png:
                    await captchaResult.Image.SaveAsPngAsync(zipStream);
                    break;
                case ImageType.Jpeg:
                    await captchaResult.Image.SaveAsJpegAsync(zipStream);
                    break;
                case ImageType.Bmp:
                    await captchaResult.Image.SaveAsBmpAsync(zipStream);
                    break;
                case ImageType.Webp:
                    await captchaResult.Image.SaveAsWebpAsync(zipStream);
                    break;
                default:
                    throw new ArgumentException("Unknown image type");
            }
        }
    }

    private static string GetImageTypeExtenstion(ImageType imageType) => imageType switch
    {
        ImageType.Png => ".png",
        ImageType.Jpeg => ".jpeg",
        ImageType.Bmp => ".bmp",
        ImageType.Webp => ".webp",
        _ => throw new Exception("Unknown image type"),
    };

    /// <summary>
    /// Create folder and combine with current path.
    /// </summary>
    public ImageSaver CreateFolder(string folderName)
    {
        _path = Directory.CreateDirectory(Path.Join(_path, folderName)).FullName;
        _folderName = folderName;
        return this;
    }
    /// <summary>
    /// Specify path for output results.
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public ImageSaver WithOutputPath(string path)
    {
        if (!Directory.Exists(path))
            throw new ArgumentException($"Dirrectory specified by path '{path}' doesnt' exist");
        _path = path;
        return this;
    }
    /// <summary>
    /// Specify type of output images.
    /// </summary>
    public ImageSaver WithOutputType(ImageType type)
    {
        _imageType = type;
        return this;
    }
    /// <summary>
    /// Specify prefix for files.
    /// </summary>
    public ImageSaver WithFilePrefix(string prefix)
    {
        _prefix = prefix;
        return this;
    }
}

public enum ImageType {
    Png,
    Jpeg,
    Bmp,
    Webp
}