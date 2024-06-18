using UsersAPI.Domain.Entities;

namespace UsersAPI.Domain.Interfaces.Services
{
  public interface IPermissionDomainService : IDisposable
  {
    void Add(Permission permission);
    void Update(Permission permission);
    void Delete(Permission permission);
    List<Permission> GetAll();
    Permission? Get(Guid id);
    Permission? Get(Guid subModuleId, Guid roleId);
  }
}
