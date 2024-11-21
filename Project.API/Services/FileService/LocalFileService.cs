using Project.API.Database;

namespace Project.API.Services.FileService;

public class LocalFileService(AppDbContext context) : IFileService
{
    public Task<string> Upload(string imageData, string fileExtension)
    {
        var data  = $"data:image/{fileExtension};base64, {imageData}";
        return Task.FromResult(data);
    }

    public Task Replace(string fileName, string fileData)
    {
        return Task.CompletedTask;
    }
}