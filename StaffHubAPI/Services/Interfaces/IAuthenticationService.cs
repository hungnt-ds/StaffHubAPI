using System.Security.Claims;
using StaffHubAPI.DataAccess.Entities;

namespace StaffHubAPI.Services.Interfaces
{
    public interface IAuthenticationService
    {
        string RefreshToken(string refreshToken);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        RefreshToken GenerateRefreshToken();
        string CreateToken(User user);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        void SetRefreshToken(User user, RefreshToken newRefreshToken);
        string GetUserName();
    }
}
