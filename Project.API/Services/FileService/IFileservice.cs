namespace Project.API.Services.FileService;

public interface IFileservice
{
    Task<string> Upload(string imageData, string fileName);

    Task<string> Get(string image);
}