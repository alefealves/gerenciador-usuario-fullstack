using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;

namespace UsersAPI.Application.Interfaces.Application
{
  public interface IUserAppService : IDisposable
  {
    UserResponseDto Add(UserAddRequestDto dto);
    UserResponseDto Update(Guid id, UserUpdateRequestDto dto);
    UserResponseDto Delete(Guid id);
    List<UserResponseDto> GetAll();
    UserResponseDto Get(Guid id);
  }
}
