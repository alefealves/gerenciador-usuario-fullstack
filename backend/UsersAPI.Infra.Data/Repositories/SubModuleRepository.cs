using Microsoft.EntityFrameworkCore;
using UsersAPI.Domain.Entities;
using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Infra.Data.Contexts;

namespace UsersAPI.Infra.Data.Repositories
{
  public class SubModuleRepository : BaseRepository<SubModule, Guid>, ISubModuleRepository
  {
    private readonly DataContext? _dataContext;

    public SubModuleRepository(DataContext? dataContext) : base(dataContext)
    {
      _dataContext = dataContext;
    }

    List<SubModule> GetAll()
    {
      return _dataContext.SubModule.Include(m => m.Module).ToList();
    }

    public List<SubModule> GetAll(Guid moduleId)
    {
      return _dataContext.SubModule.Include(m => m.Module).Where(s => s.ModuleId.Equals(moduleId)).ToList();
    }

    public SubModule? Get(string subModuleName)
    {
      return _dataContext?.SubModule.Include(m => m.Module).Where(s => s.SubModuleName.Equals(subModuleName)).FirstOrDefault();
    }

    public SubModule? GetByModuleId(Guid moduleId)
    {
      return _dataContext.SubModule.Include(m => m.Module).Where(s => s.ModuleId.Equals(moduleId)).FirstOrDefault();
    }

    public Module? GetExistsModuleId(Guid moduleId)
    {
      return _dataContext.Module.Where(s => s.Id.Equals(moduleId)).FirstOrDefault();
    }

    public Permission? GetByPermission(Guid id)
    {
      return _dataContext.Permission.Include(s => s.SubModule).Where(s => s.SubModuleId.Equals(id)).FirstOrDefault();
    }

    public SubModule? GetById(Guid id)
    {
      return _dataContext?.SubModule.Include(m => m.Module).Where(s => s.Id.Equals(id)).FirstOrDefault();
    }
  }
}
