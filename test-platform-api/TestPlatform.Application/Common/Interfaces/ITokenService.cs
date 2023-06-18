using System.Security.Claims;
using TestPlatform.Core.Models;

namespace TestPlatform.Application.Common.Interfaces;

public interface ITokenService
{
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    string GenerateAccessToken(User user);
    
    string GenerateRefreshToken();
}