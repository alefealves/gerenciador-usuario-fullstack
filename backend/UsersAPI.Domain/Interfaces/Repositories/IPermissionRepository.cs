using UsersAPI.Domain.Entities;

namespace UsersAPI.Domain.Interfaces.Repositories
{
  public interface IPermissionRepository : IBaseRepository<Permission, Guid>
  {
    List<Permission> GetAll();
    Permission? Get(Guid subModuleId, Guid roleId);
    Permission? GetById(Guid id);
  }
}
