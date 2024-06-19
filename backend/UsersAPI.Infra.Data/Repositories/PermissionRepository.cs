using Microsoft.EntityFrameworkCore;
using UsersAPI.Domain.Entities;
using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Infra.Data.Contexts;

namespace UsersAPI.Infra.Data.Repositories
{
  public class PermissionRepository : BaseRepository<Permission, Guid>, IPermissionRepository
  {
    private readonly DataContext? _dataContext;

    public PermissionRepository(DataContext? dataContext) : base(dataContext)
    {
      _dataContext = dataContext;
    }

    public List<Permission> GetAll()
    {
      return _dataContext.Permission.Include(r => r.Role).Include(s => s.SubModule).ToList();
    }

    public Permission? Get(Guid subModuleId, Guid roleId)
    {
      return _dataContext?.Permission.Where(p => p.SubModuleId == subModuleId && p.RoleId == roleId).FirstOrDefault();
    }

    public Permission? GetById(Guid id)
    {
      return _dataContext?.Permission.Include(r => r.Role).Include(s => s.SubModule).Where(p => p.Id.Equals(id)).FirstOrDefault();
    }
  }
}
