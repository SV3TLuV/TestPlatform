using TestPlatform.Application.Common.Interfaces;

namespace TestPlatform.Application.Services;

public sealed class PasswordHasherService : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool Check(string hash, string password)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
    }
}