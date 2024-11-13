namespace Project.API.Services.FileService;

public interface IFileService
{
    /// <summary>
    /// Upload file
    /// </summary>
    /// <param name="imageData">Image data in Base64</param>
    /// <param name="fileName">File name with extension</param>
    /// <returns>Path in string</returns>
    Task<string> Upload(string imageData, string fileName);

    Task<string> Get(string image);
}