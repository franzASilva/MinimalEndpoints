using MinimalEndpoints.Domain.Services.Interfaces;
using System.Text;

namespace MinimalEndpoints.Domain.Services;

public sealed class PasswordHashService : IPasswordHashService
{
    public string HashSHA1(string value)
    {
        var inputBytes = Encoding.ASCII.GetBytes(value);
        var hash = System.Security.Cryptography.SHA1.HashData(inputBytes);
        var sb = new StringBuilder();

        for (var i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }

        return sb.ToString();
    }

    public bool VerifiesPassword(string password, string passwordHash, string guid)
    {
        var hashedPassword = HashSHA1(password + guid);
        return string.Compare(passwordHash, hashedPassword) == 0;
    }
}
