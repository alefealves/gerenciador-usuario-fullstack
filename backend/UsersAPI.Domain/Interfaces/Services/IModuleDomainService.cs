using UsersAPI.Domain.Entities;

namespace UsersAPI.Domain.Interfaces.Services
{
  public interface IModuleDomainService : IDisposable
  {
    void Add(Module module);
    void Update(Module module);
    void Delete(Module module);
    List<Module> GetAll();
    Module? Get(Guid id);
    Module? Get(string moduleName);
  }
}
