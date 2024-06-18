using UsersAPI.Domain.Entities;
using UsersAPI.Domain.Exceptions;
using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Domain.Interfaces.Services;

namespace UsersAPI.Domain.Services
{
  public class PermissionDomainService : IPermissionDomainService
  {
    private readonly IUnitOfWork? _unitOfWork;

    public PermissionDomainService(IUnitOfWork? unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public void Add(Permission permission)
    {
      if (Get(permission.SubModuleId, permission.RoleId) != null)
        throw new PermissionExistsException();

      _unitOfWork?.PermissionRepository.Add(permission);
      _unitOfWork?.SaveChanges();
    }

    public void Update(Permission permission)
    {
      _unitOfWork?.PermissionRepository.Update(permission);
      _unitOfWork?.SaveChanges();
    }

    public void Delete(Permission permission)
    {
      _unitOfWork?.PermissionRepository.Delete(permission);
      _unitOfWork?.SaveChanges();
    }

    public List<Permission> GetAll()
    {
      return _unitOfWork?.PermissionRepository.GetAll().ToList();
    }

    public Permission? Get(Guid id)
    {
      return _unitOfWork?.PermissionRepository.GetById(id);
    }
    public Permission? Get(Guid subModuleId, Guid roleId)
    {
      return _unitOfWork?.PermissionRepository.Get(p => p.SubModuleId == subModuleId && p.RoleId == roleId);
    }

    public void Dispose()
    {
      _unitOfWork.Dispose();
    }
  }
}
