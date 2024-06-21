using UsersAPI.Domain.Entities;
using UsersAPI.Domain.Exceptions;
using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Domain.Interfaces.Services;
using UsersAPI.Domain.Interfaces.Security;
using UsersAPI.Domain.ValueObjects;

namespace UsersAPI.Domain.Services
{
  public class UserDomainService : IUserDomainService
  {
    private readonly IUnitOfWork? _unitOfWork;
    private readonly ITokenService? _tokenService;

    //private readonly IUserMessageProducer? userMessageProducer;

    // public UserDomainService(IUnitOfWork? unitOfWork, IUserMessageProducer? userMessageProducer)
    // {
    //   _unitOfWork = unitOfWork;
    //   this.userMessageProducer = userMessageProducer;
    // }

    public UserDomainService(IUnitOfWork? unitOfWork, ITokenService? tokenService)
    {
      _unitOfWork = unitOfWork;
      _tokenService = tokenService;
    }

    public void Add(User user)
    {
      if (Get(user.Email) != null)
        throw new EmailAlreadyExistsException(user.Email);

      if (_unitOfWork.RoleRepository.GetById(user.RoleId) == null)
        throw new RoleIsNotExistsException(user.RoleId);

      _unitOfWork?.UserRepository.Add(user);
      _unitOfWork?.SaveChanges();

      // userMessageProducer?.Send(new UserMessageVO
      // {
      //   Email = user.Email,
      //   Subject = "Parabéns, sua conta de usuário foi criada com sucesso",
      //   Body = @$"Olá {user.FirstName}, clique no link para ativar seu Usuário em nosso sistema."
      // });
    }

    public void Update(User user)
    {
      if (GetByRoleId(user.RoleId) == null)
        throw new RoleIsNotExistsException(user.RoleId);

      _unitOfWork?.UserRepository.Update(user);
      _unitOfWork?.SaveChanges();
    }

    public void Delete(User user)
    {
      _unitOfWork?.UserRepository.Delete(user);
      _unitOfWork?.SaveChanges();
    }

    public List<User> GetAll()
    {
      return _unitOfWork?.UserRepository.GetAll().ToList();
    }

    public User? Get(Guid id)
    {
      return _unitOfWork?.UserRepository.GetById(id);
    }

    public User? Get(string email)
    {
      return _unitOfWork?.UserRepository.Get(u => u.Email.Equals(email));
    }

    public User? Get(string email, string password)
    {
      return _unitOfWork?.UserRepository.Get(u => u.Email.Equals(email) && u.Password.Equals(password));
    }

    public User? GetByRoleId(Guid roleId)
    {
      return _unitOfWork?.UserRepository.Get(u => u.RoleId.Equals(roleId));
    }

    public string Authenticate(string email, string password)
    {
      var user = Get(email, password);

      if (user == null)
        throw new AccessDeniedException();

      var userAuth = new UserAuthVO
      {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        Role = user.Role.RoleName,
        SignedAt = DateTime.Now
      };

      return _tokenService?.CreateToken(userAuth);
    }

    public void Dispose()
    {
      _unitOfWork?.Dispose();
    }
  }
}
