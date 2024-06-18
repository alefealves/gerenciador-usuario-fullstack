using UsersAPI.Domain.Entities;

namespace UsersAPI.Domain.Interfaces.Services
{
  public interface ISubModuleDomainService : IDisposable
  {
    void Add(SubModule subModule);
    void Update(SubModule subModule);
    void Delete(SubModule subModule);
    List<SubModule> GetAll();
    List<SubModule> GetAll(Guid moduleId);
    SubModule? Get(string subModuleName);
    SubModule? GetById(Guid id);
    SubModule? GetByModuleId(Guid moduleId);
    SubModule? GetByPermission(Guid id);
  }
}
