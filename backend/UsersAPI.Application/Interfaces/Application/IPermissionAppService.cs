using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;

namespace UsersAPI.Application.Interfaces.Application
{
  public interface IPermissionAppService : IDisposable
  {
    PermissionResponseDto Add(PermissionAddRequestDto dto);
    PermissionResponseDto Update(Guid id, PermissionUpdateRequestDto dto);
    PermissionResponseDto Delete(Guid id);
    List<PermissionResponseDto> GetAll();
    PermissionResponseDto? Get(Guid id);
    PermissionResponseDto? Get(Guid subModuleId, Guid roleId);
  }
}
