using System.Security.Cryptography;
using System.Text;

namespace Project.API.Services.Utils;

public class SimpleHasher
{
    public static string HashPassword(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = SHA256.HashData(passwordBytes);
        return Convert.ToBase64String(hashBytes);
    }
}