using UsersAPI.Domain.ValueObjects;

namespace UsersAPI.Domain.Interfaces.Security
{
  public interface ITokenService
  {
    UserAuthVO CreateToken(UserAuthVO user);
  }
}
