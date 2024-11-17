using Project.API.Database;

namespace Project.API.Services.FileService;

public class LocalFileService(AppDbContext context) : IFileService
{
    public Task<string> Upload(string imageData, string fileExtension)
    {
        return Task.FromResult(imageData);
    }

    public Task Replace(string fileName, string fileData)
    {
        return Task.CompletedTask;
    }
}