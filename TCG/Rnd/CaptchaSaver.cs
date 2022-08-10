using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Rnd;

public class CaptchaSaver
{
    private string _path = "";
    private string _folderName = "";
    private string _prefix = "";
    private ImageType _imageType = ImageType.Png;
    private IEnumerable<CaptchaResult> _captchaResults;

    public CaptchaSaver(IEnumerable<CaptchaResult> captchaResults)
    {
        _captchaResults = captchaResults;
    }

    public CaptchaSaver(IEnumerable<CaptchaResult> captchaResults, ImageType imageType) : this(captchaResults)
    {
        _imageType = imageType;
    }

    public CaptchaSaver(IEnumerable<CaptchaResult> captchaResults, string path, ImageType imageType) : this(captchaResults, imageType)
    {
        _path = path;
    }

    public void Save()
    {
        foreach(CaptchaResult captchaResult in _captchaResults)
        {
            string resPath = Path.Join(_path, _prefix + captchaResult.GetName() + GetImageTypeExtenstion(_imageType));
            switch (_imageType)
            {
                case ImageType.Png:
                    captchaResult.Image.SaveAsPng(resPath);
                    break;
                case ImageType.Jpeg:
                    captchaResult.Image.SaveAsJpeg(resPath);
                    break;
                case ImageType.Bmp:
                    captchaResult.Image.SaveAsBmp(resPath);
                    break;
                case ImageType.Webp:
                    captchaResult.Image.SaveAsWebp(resPath);
                    break;
                default:
                    throw new Exception("Unknown image type");
            }
        }
    }


    public async Task SaveAsync()
    {
        foreach (CaptchaResult captchaResult in _captchaResults)
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
                default:
                    throw new Exception("Unknown image type");
            }
        }
    }

    public void SaveAsZip(string archiveName = "archive", CompressionLevel compressionLevel = CompressionLevel.Fastest)
    {
        using (var archiveStream = new FileStream(Path.Join(_path, archiveName + ".zip"), FileMode.Create))
        {
            using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
            {
                foreach (CaptchaResult captchaResult in _captchaResults)
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
                        default:
                            throw new Exception("Unknown image type");
                    }
                }
            }
        }
    }

    public async Task SaveAsZipAsync(string archiveName = "archive", CompressionLevel compressionLevel = CompressionLevel.Fastest)
    {
        using (var archiveStream = new FileStream(Path.Join(_path, archiveName + ".zip"), FileMode.CreateNew, FileAccess.Write))
        {
            using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
            {
                foreach (CaptchaResult captchaResult in _captchaResults)
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
                            throw new Exception("Unknown image type");
                    }
                }
            }
        }
    }

    private static string GetImageTypeExtenstion(ImageType imageType)
    {
        switch (imageType)
        {
            case ImageType.Png:
                return ".png";
            case ImageType.Jpeg:
                return ".jpeg";
            case ImageType.Bmp:
                return ".bmp";
            case ImageType.Webp:
                return ".webp";
            default:
                throw new Exception("Unknown image type");
        }
    }


    public CaptchaSaver CreateFolder(string folderName)
    {
        _path = Directory.CreateDirectory(Path.Join(_path, folderName)).FullName;
        _folderName = folderName;
        return this;
    }

    public CaptchaSaver WithOutputPath(string path)
    {
        if (!Directory.Exists(path))
            throw new ArgumentException($"Dirrectory specified by path '{path}' doesnt' exist");
        _path = path;
        return this;
    }

    public CaptchaSaver WithOutputType(ImageType type)
    {
        _imageType = type;
        return this;
    }

    public CaptchaSaver WithFilePrefix(string prefix)
    {
        _prefix = prefix;
        return this;
    }
}

public enum ImageType { Png, Jpeg, Bmp, Webp }

// set output type
public interface ISetOutputType
{
    public IHasTypeSetOutputPath SetOutputType(ImageType type);
}

// set path
public interface IHasTypeSetOutputPath
{
    public IHasTypeAndPath SetOutputPath(string path);
}

// choose save type
public interface IHasTypeAndPath
{
    public ISeparateFileSaver SaveAsSeparateFile();
    public IZipFileSaver SaveAsZip(string archiveName);
}

// separate file type
public interface ISeparateFileSaver : ISaveSeparateFiles
{
    public ISeparateFileSaverWithFilePrefix WithFilePrefix(string prefix);
    public ISeparateFileSaverWithFolder CreateFolder(string folderName);
}

public interface ISeparateFileSaverWithFilePrefix : ISaveSeparateFiles
{
    public ISaveSeparateFiles CreateFolder(string folderName);
}

public interface ISeparateFileSaverWithFolder : ISaveSeparateFiles
{
    public ISaveSeparateFiles WithFilePrefix(string prefix);
}

public interface ISaveSeparateFiles
{
    public void Save();
    public Task SaveAsync();
}

// zip file type
public interface IZipFileSaver : ISaveZipFile
{
    public ISaveZipFile WithFilePrefix(string prefix);
}
public interface ISaveZipFile
{
    public void Save();
    public Task SaveAsync();
}