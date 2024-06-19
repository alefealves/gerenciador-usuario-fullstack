using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;

namespace UsersAPI.Application.Interfaces.Application
{
  public interface IRoleAppService : IDisposable
  {
    RoleResponseDto Add(RoleAddRequestDto dto);
    RoleResponseDto Update(Guid id, RoleUpdateRequestDto dto);
    RoleResponseDto Delete(Guid id);
    List<RoleResponseDto> GetAll();
    RoleResponseDto Get(Guid id);
  }
}
