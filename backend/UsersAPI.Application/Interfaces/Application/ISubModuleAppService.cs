using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;

namespace UsersAPI.Application.Interfaces.Application
{
  public interface ISubModuleAppService : IDisposable
  {
    SubModuleResponseDto Add(SubModuleAddRequestDto dto);
    SubModuleResponseDto Update(Guid id, SubModuleUpdateRequestDto dto);
    SubModuleResponseDto Delete(Guid id);
    List<SubModuleResponseDto> GetAll();
    List<SubModuleResponseDto> GetAll(Guid moduleId);
    SubModuleResponseDto Get(Guid id);
    SubModuleResponseDto GetByModuleId(Guid moduleId);
  }
}
