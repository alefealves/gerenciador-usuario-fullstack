using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersAPI.Domain.Interfaces.Security;
using UsersAPI.Domain.ValueObjects;
using UsersAPI.Infra.Security.Settings;

namespace UsersAPI.Infra.Security.Services
{
  public class TokenService : ITokenService
  {
    private readonly TokenSettings? tokenSettings;

    public TokenService(IOptions<TokenSettings?> tokenSettings)
    {
      this.tokenSettings = tokenSettings.Value;
    }

    public UserAuthVO CreateToken(UserAuthVO user)
    {
      try
      {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, JsonConvert.SerializeObject(user)),
            new Claim(ClaimTypes.Role, user?.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: tokenSettings.Issuer,
            audience: tokenSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(tokenSettings.ExpirationInMinutes)),
            signingCredentials: credentials
            );

        var tokenHandler = new JwtSecurityTokenHandler();

        var userAuth = new UserAuthVO
        {
          Id = user.Id,
          FirstName = user.FirstName,
          LastName = user.LastName,
          Email = user.Email,
          Role = user.Role,
          AccessToken = tokenHandler.WriteToken(jwtSecurityToken),
          Expiration = DateTime.Now.AddMinutes(Convert.ToDouble(tokenSettings.ExpirationInMinutes)),
          SignedAt = DateTime.Now
        };

        return userAuth;
      }
      catch (Exception e)
      {
        throw new Exception($"Error: {e.Message}, reporte ao Administrador do sistema o erro informado.");
      }
    }
  }
}
