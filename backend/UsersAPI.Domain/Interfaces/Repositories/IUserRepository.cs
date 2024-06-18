using UsersAPI.Domain.Entities;

namespace UsersAPI.Domain.Interfaces.Repositories
{
  public interface IUserRepository : IBaseRepository<User, Guid>
  {
    List<User> GetAll();
    User? GetById(Guid id);
    User? GetByRoleId(Guid roleId);
  }
}
