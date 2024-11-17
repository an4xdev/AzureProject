namespace Project.API.Services.FileService;

public interface IFileService
{
    /// <summary>
    /// Upload file
    /// </summary>
    /// <param name="imageData">Image data in Base64 encoding</param>
    /// <param name="fileExtension">Extension of photo</param>
    /// <returns>Path in string</returns>
    Task<string> Upload(string imageData, string fileExtension);

    /// <summary>
    /// Replace existing image and return path.
    /// </summary>
    /// <param name="fileName">Old file which is saved in Base64 in local and Guid.extension in Azure Storage service</param>
    /// <param name="fileData">New image data in Base64 Encoding</param>
    /// <returns>Base64 in local, path in Azure Storage</returns>
    Task Replace(string fileName, string fileData);
}