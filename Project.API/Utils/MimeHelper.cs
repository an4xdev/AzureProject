namespace Project.API.Utils;

public static class MimeHelper
{
    private static readonly Dictionary<string, string> MimeMappings = new(StringComparer.InvariantCultureIgnoreCase)
    {
        { ".jpg", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".png", "image/png" },
        { ".gif", "image/gif" },
        { ".bmp", "image/bmp" },
        { ".svg", "image/svg+xml" },
        { ".webp", "image/webp" }
    };

    public static string GetMimeType(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        return MimeMappings.GetValueOrDefault(extension, "application/octet-stream");
    }
}

