using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;

namespace UsersAPI.Application.Interfaces.Application
{
  public interface IModuleAppService : IDisposable
  {
    ModuleResponseDto Add(ModuleAddRequestDto dto);
    ModuleResponseDto Update(Guid id, ModuleUpdateRequestDto dto);
    ModuleResponseDto Delete(Guid id);
    List<ModuleResponseDto> GetAll();
    ModuleResponseDto Get(Guid id);
  }
}
