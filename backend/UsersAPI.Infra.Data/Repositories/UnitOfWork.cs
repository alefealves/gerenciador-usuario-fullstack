using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Infra.Data.Contexts;

namespace UsersAPI.Infra.Data.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly DataContext? _dataContext;

    public UnitOfWork(DataContext? dataContext)
    {
      _dataContext = dataContext;
    }
    public IModuleRepository ModuleRepository => new ModuleRepository(_dataContext);
    public IPermissionRepository PermissionRepository => new PermissionRepository(_dataContext);
    public IRoleRepository RoleRepository => new RoleRepository(_dataContext);
    public ISubModuleRepository SubModuleRepository => new SubModuleRepository(_dataContext);
    public IUserRepository UserRepository => new UserRepository(_dataContext);

    public void SaveChanges()
    {
      _dataContext?.SaveChanges();
    }

    public void Dispose()
    {
      _dataContext?.Dispose();
    }
  }
}
