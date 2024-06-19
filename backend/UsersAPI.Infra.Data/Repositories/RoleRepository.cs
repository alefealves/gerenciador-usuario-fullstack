using UsersAPI.Domain.Entities;
using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Infra.Data.Contexts;

namespace UsersAPI.Infra.Data.Repositories
{
  public class RoleRepository : BaseRepository<Role, Guid>, IRoleRepository
  {
    private readonly DataContext? _dataContext;

    public RoleRepository(DataContext? dataContext) : base(dataContext)
    {
      _dataContext = dataContext;
    }
  }
}
