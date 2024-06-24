using UsersAPI.Domain.Entities;

namespace UsersAPI.Domain.Interfaces.Repositories
{
  public interface ISubModuleRepository : IBaseRepository<SubModule, Guid>
  {
    List<SubModule> GetAll();
    List<SubModule> GetAll(Guid moduleId);
    SubModule? Get(string subModuleName);
    SubModule? GetById(Guid id);
    SubModule? GetByModuleId(Guid moduleId);
    Permission? GetByPermission(Guid id);
    Module? GetExistsModuleId(Guid moduleId);
  }
}
