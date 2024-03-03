using Microsoft.AspNetCore.Identity;

namespace NZWalksAPI.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
