using System.Security.Claims;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.Services.Interfaces;

public interface ITokenService
{
    List<Claim> CreateClaims(UserEntity user, List<RoleEntity> roles);
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
}