using Project.API.Utils;

namespace Project.API.Services.FileService;

public class LocalFileService : IFileService
{
    public async Task<string> Upload(string imageData, string fileName)
    {
        var imageBytes = Convert.FromBase64String(imageData);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);

        var directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await File.WriteAllBytesAsync(filePath, imageBytes);

        return filePath;
    }

    public async Task<string> Get(string image)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", image);

        var mime = MimeHelper.GetMimeType(image);

        if (!File.Exists(filePath))
        {
            return await Task.FromResult(string.Empty);
        }

        var fileBytes = await File.ReadAllBytesAsync(filePath);
        var base64String = Convert.ToBase64String(fileBytes);

        return await Task.FromResult($"data:image/{mime};base64,{base64String}");
    }
}