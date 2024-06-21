using AutoMapper;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Application.Interfaces.Application;
using UsersAPI.Domain.Interfaces.Services;
using UsersAPI.Domain.Exceptions;

namespace UsersAPI.Application.Services
{
  public class AuthAppService : IAuthAppService
  {
    private readonly IUserDomainService? _userDomainService;
    private readonly IMapper? _mapper;

    public AuthAppService(IUserDomainService? userDomainService, IMapper? mapper)
    {
      _userDomainService = userDomainService;
      _mapper = mapper;
    }

    public LoginResponseDto Login(LoginRequestDto dto)
    {
      try
      {
        var token = _userDomainService?.Authenticate(dto.Email, dto.Password);

        return new LoginResponseDto
        {
          AccessToken = token
        };
      }
      catch (AccessDeniedException aex)
      {
        throw new ApplicationException(aex.Message);
      }
    }

    public UserResponseDto ForgotPassword(ForgotPasswordRequestDto dto)
    {
      var user = _userDomainService?.Get(dto.Email);
      //TODO Implementar a recuperação da senha do usuário
      return _mapper.Map<UserResponseDto>(user);
    }

    public UserResponseDto ResetPassword(Guid id, ResetPasswordRequestDto dto)
    {
      var user = _userDomainService?.Get(id);
      //TODO Implementar a atualização da senha do usuário
      return _mapper.Map<UserResponseDto>(user);
    }

    public UserResponseDto ActivateUser(Guid id, ActivateUserRequestDto dto)
    {
      try
      {
        if ((id == Guid.Empty) && (dto.Id == Guid.Empty))
          throw new Exception("O ID do usuário deve ser informado.");

        if (id != Guid.Empty)
        {
          var user = _userDomainService?.Get(id);
          user.Active = true;
          user.EmailConfirmed = true;

          _userDomainService?.Update(user);

          return _mapper.Map<UserResponseDto>(user);
        }
        else
        {
          var user = _userDomainService?.Get(dto.Id);
          user.Active = true;
          user.EmailConfirmed = true;

          _userDomainService?.Update(user);

          return _mapper.Map<UserResponseDto>(user);
        }
      }
      catch (Exception e)
      {
        throw new Exception($"Error: {e.Message}, reporte ao Administrador do sistema o erro informado.");
      }
    }
    public void Dispose()
    {
      _userDomainService?.Dispose();
    }
  }
}
