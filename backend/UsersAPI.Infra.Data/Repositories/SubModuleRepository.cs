using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Reflection;
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

    // List<SubModule> GetAll()
    // {
    //   return _dataContext.SubModule.Include(m => m.Module).ToList();
    // }

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

    public SubModule? GetByPermission(Guid id)
    {
      var consulta =
          from p in _dataContext.Permission
          join s in _dataContext.SubModule on p.SubModuleId equals s.Id
          where (s.Id == id)
          select new SubModule
          {
            Id = s.Id
          };

      return consulta.FirstOrDefault();
    }
    SubModule? ISubModuleRepository.GetById(Guid id)
    {
      return _dataContext?.SubModule.Include(m => m.Module).Where(s => s.Id.Equals(id)).FirstOrDefault();
    }
  }
}
