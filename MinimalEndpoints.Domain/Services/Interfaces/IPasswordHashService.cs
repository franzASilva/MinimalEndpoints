namespace MinimalEndpoints.Domain.Services.Interfaces;

public interface IPasswordHashService
{
    string HashSHA1(string value);
    bool VerifiesPassword(string password, string passwordHash, string guid);
}
