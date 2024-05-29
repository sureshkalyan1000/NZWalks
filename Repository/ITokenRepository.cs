using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace api8.Repository
{
    public interface ITokenRepository
    {
        String CreateJwtToken(IdentityUser user, List<string> roles);
        
    }
}
