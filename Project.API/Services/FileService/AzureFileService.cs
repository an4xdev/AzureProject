using Azure.Storage.Blobs;

namespace Project.API.Services.FileService;

public class AzureFileService : IFileService
{

    private readonly string _storageKey = Environment.GetEnvironmentVariable("STORAGE_KEY") ?? "";

    public async Task<string> Upload(string imageData, string fileExtension)
    {
        var uploadFileName = Guid.NewGuid() + fileExtension;

        var fileBytes = Convert.FromBase64String(imageData);
        var stream = new MemoryStream(fileBytes);

        BlobServiceClient blobServiceClient = new(_storageKey);

        var containerClient = blobServiceClient.GetBlobContainerClient("test");

        var blobClient = containerClient.GetBlobClient($"{uploadFileName}");

        await blobClient.UploadAsync(stream);

        return await Task.FromResult(blobClient.Uri.ToString());
    }

    public async Task Replace(string fileName, string imageData)
    {
        var fileBytes = Convert.FromBase64String(imageData);
        var stream = new MemoryStream(fileBytes);

        BlobServiceClient blobServiceClient = new(_storageKey);

        var containerClient = blobServiceClient.GetBlobContainerClient("test");

        var blobClient = containerClient.GetBlobClient($"{fileName}");

        await blobClient.UploadAsync(stream, overwrite:true);
    }

    public async Task Delete(string filePath)
    {
        var blobClient = new BlobClient(new Uri(filePath));

        await blobClient.DeleteIfExistsAsync();
    }
}