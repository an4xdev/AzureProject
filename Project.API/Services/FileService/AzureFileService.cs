using Azure.Storage.Blobs;
using Project.Shared.DoNotGit;

namespace Project.API.Services.FileService;

public class AzureFileService : IFileService
{
    public async Task<string> Upload(string imageData, string fileExtension)
    {
        var uploadFileName = Guid.NewGuid() + fileExtension;

        var fileBytes = Convert.FromBase64String(imageData);
        var stream = new MemoryStream(fileBytes);

        BlobServiceClient blobServiceClient = new(DontCommit.StorageKey);

        var containerClient = blobServiceClient.GetBlobContainerClient("test");

        var blobClient = containerClient.GetBlobClient($"{uploadFileName}");

        await blobClient.UploadAsync(stream);

        return await Task.FromResult(blobClient.Uri.ToString());
    }

    public async Task Replace(string fileName, string imageData)
    {
        var fileBytes = Convert.FromBase64String(imageData);
        var stream = new MemoryStream(fileBytes);

        BlobServiceClient blobServiceClient = new(DontCommit.StorageKey);

        var containerClient = blobServiceClient.GetBlobContainerClient("test");

        var blobClient = containerClient.GetBlobClient($"{fileName}");

        await blobClient.UploadAsync(stream, overwrite:true);
    }

}