using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Rnd;

public class CaptchaSaver : ISaveSeparateFiles, ISaveZipFile, 
    ISetOutputType, IHasTypeSetOutputPath, IHasTypeAndPath, 
    ISeparateFileSaver, ISeparateFileSaverWithFilePrefix, ISeparateFileSaverWithFolder,
    IZipFileSaver
{
    private string _path = "";
    private string _prefix = "";
    private string _folderName = "";
    private ImageType _imageType = ImageType.Png;

    private CaptchaSaver() { }

    public static ISetOutputType Create()
    {
        return new CaptchaSaver();
    }

    void ISaveSeparateFiles.Save(IEnumerable<CaptchaResult> captchaResults)
    {
        foreach(CaptchaResult captchaResult in captchaResults)
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


    async Task ISaveSeparateFiles.SaveAsync(IEnumerable<CaptchaResult> captchaResults)
    {
        foreach (CaptchaResult captchaResult in captchaResults)
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

    void ISaveZipFile.Save(IEnumerable<CaptchaResult> captchaResults)
    {
        using (var archiveStream = new FileStream(_path + ".zip", FileMode.Create))
        {
            using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
            {
                foreach (CaptchaResult captchaResult in captchaResults)
                {
                    var zipArchiveEntry = archive.CreateEntry(captchaResult.GetName() + GetImageTypeExtenstion(_imageType), CompressionLevel.Fastest);
                    
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

    async Task ISaveZipFile.SaveAsync(IEnumerable<CaptchaResult> captchaResults)
    {
        using (var archiveStream = new FileStream(_path + ".zip", FileMode.CreateNew, FileAccess.Write))
        {
            using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
            {
                foreach (CaptchaResult captchaResult in captchaResults)
                {
                    var zipArchiveEntry = archive.CreateEntry(captchaResult.GetName() + GetImageTypeExtenstion(_imageType), CompressionLevel.Fastest);

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


    public ISeparateFileSaver CreateFolder(string folderName)
    {
        CreateFolderInternal(folderName);
        return this;
    }

    public ISeparateFileSaver SaveAsSeparateFile()
    {
        return this;
    }

    public IZipFileSaver SaveAsZip(string archiveName)
    {
        _path = Path.Combine(_path, archiveName);
        return this;
    }

    public IHasTypeAndPath SetOutputPath(string path)
    {
        if (!Directory.Exists(path))
            throw new ArgumentException($"Dirrectory specified by path '{path}' doesnt' exist");
        _path = path;
        return this;
    }

    public IHasTypeSetOutputPath SetOutputType(ImageType type)
    {
        _imageType = type;
        return this;
    }

    public ISeparateFileSaverWithFilePrefix WithFilePrefix(string prefix)
    {
        _prefix = prefix;
        return this;
    }

    ISaveSeparateFiles ISeparateFileSaverWithFilePrefix.CreateFolder(string folderName)
    {
        CreateFolderInternal(folderName);
        return this;
    }

    
    ISaveSeparateFiles ISeparateFileSaverWithFolder.WithFilePrefix(string prefix)
    {
        _prefix = prefix;
        return this;
    }

    ISaveZipFile IZipFileSaver.WithFilePrefix(string prefix)
    {
        _prefix = prefix;
        return this;
    }

    ISeparateFileSaverWithFolder ISeparateFileSaver.CreateFolder(string folderName)
    {
        CreateFolderInternal(folderName);
        return this;
    }

    private void CreateFolderInternal(string folderName)
    {
        _path = Directory.CreateDirectory(Path.Join(_path, folderName)).FullName;
        _folderName = folderName;
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
    public void Save(IEnumerable<CaptchaResult> captchaResults);
    public Task SaveAsync(IEnumerable<CaptchaResult> captchaResults);
}

// zip file type
public interface IZipFileSaver : ISaveZipFile
{
    public ISaveZipFile WithFilePrefix(string prefix);
}
public interface ISaveZipFile
{
    public void Save(IEnumerable<CaptchaResult> captchaResults);
    public Task SaveAsync(IEnumerable<CaptchaResult> captchaResults);
}