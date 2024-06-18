using UsersAPI.Domain.Entities;
using UsersAPI.Domain.Exceptions;
using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Domain.Interfaces.Services;

namespace UsersAPI.Domain.Services
{
  public class RoleDomainService : IRoleDomainService
  {
    private readonly IUnitOfWork? _unitOfWork;

    public RoleDomainService(IUnitOfWork? unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public void Add(Role role)
    {
      if (Get(role.RoleName) != null)
        throw new RoleAlreadyExistsException(role.RoleName);

      _unitOfWork?.RoleRepository.Add(role);
      _unitOfWork?.SaveChanges();
    }

    public void Update(Role role)
    {
      _unitOfWork?.RoleRepository.Update(role);
      _unitOfWork?.SaveChanges();
    }

    public void Delete(Role role)
    {
      if (_unitOfWork?.UserRepository.GetByRoleId(role.Id) != null)
        throw new RoleIsNotDeleteException(role.RoleName);

      _unitOfWork?.RoleRepository.Delete(role);
      _unitOfWork?.SaveChanges();
    }

    public List<Role> GetAll()
    {
      return _unitOfWork.RoleRepository.GetAll().ToList();
    }

    public Role? Get(Guid id)
    {
      return _unitOfWork?.RoleRepository.GetById(id);
    }

    public Role? Get(string roleName)
    {
      return _unitOfWork?.RoleRepository.Get(r => r.RoleName.Equals(roleName));
    }

    public void Dispose()
    {
      _unitOfWork?.Dispose();
    }
  }
}
