using UsersAPI.Domain.Entities;

namespace UsersAPI.Domain.Interfaces.Services
{
  public interface IRoleDomainService : IDisposable
  {
    void Add(Role role);
    void Update(Role role);
    void Delete(Role role);
    List<Role> GetAll();
    Role? Get(Guid id);
    Role? Get(string roleName);
  }
}
