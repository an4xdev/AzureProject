namespace Project.API.Services.FileService;

public interface IFileService
{
    /// <summary>
    /// Upload file
    /// </summary>
    /// <param name="imageData">Image data in Base64 encoding</param>
    /// <param name="fileName">File name with extension</param>
    /// <returns>Path in string</returns>
    Task<string> Upload(string imageData, string fileName);

    /// <summary>
    /// Get image data based on file name
    /// </summary>
    /// <param name="image">File name</param>
    /// <returns>Image in Base64 encoding</returns>
    Task<string> Get(string image);
}