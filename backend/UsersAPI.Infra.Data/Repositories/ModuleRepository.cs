using UsersAPI.Domain.Entities;
using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Infra.Data.Contexts;

namespace UsersAPI.Infra.Data.Repositories
{
  public class ModuleRepository : BaseRepository<Module, Guid>, IModuleRepository
  {
    private readonly DataContext? _dataContext;

    public ModuleRepository(DataContext? dataContext) : base(dataContext)
    {
      _dataContext = dataContext;
    }
  }
}
