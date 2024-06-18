using UsersAPI.Domain.Entities;
using UsersAPI.Domain.Exceptions;
using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Domain.Interfaces.Services;

namespace UsersAPI.Domain.Services
{
  public class ModuleDomainService : IModuleDomainService
  {
    private readonly IUnitOfWork? _unitOfWork;

    public ModuleDomainService(IUnitOfWork? unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public void Add(Module module)
    {
      if (Get(module.ModuleName) != null)
        throw new ModuleAlreadyExistsException(module.ModuleName);

      _unitOfWork?.ModuleRepository.Add(module);
      _unitOfWork?.SaveChanges();
    }

    public void Update(Module module)
    {
      _unitOfWork?.ModuleRepository.Update(module);
      _unitOfWork?.SaveChanges();
    }

    public void Delete(Module module)
    {
      if (_unitOfWork?.SubModuleRepository.GetByModuleId(module.Id) != null)
        throw new ModuleIsNotDeleteException(module.ModuleName);

      _unitOfWork?.ModuleRepository.Delete(module);
      _unitOfWork?.SaveChanges();
    }

    public List<Module> GetAll()
    {
      return _unitOfWork?.ModuleRepository.GetAll().ToList();
    }

    public Module? Get(Guid id)
    {
      return _unitOfWork?.ModuleRepository.GetById(id);
    }

    public Module? Get(string moduleName)
    {
      return _unitOfWork?.ModuleRepository.Get(m => m.ModuleName.Equals(moduleName));
    }

    public void Dispose()
    {
      _unitOfWork?.Dispose();
    }
  }
}
