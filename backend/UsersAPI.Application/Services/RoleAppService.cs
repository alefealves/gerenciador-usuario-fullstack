using AutoMapper;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Application.Interfaces.Application;
using UsersAPI.Domain.Entities;
using UsersAPI.Domain.Exceptions;
using UsersAPI.Domain.Interfaces.Services;

namespace UsersAPI.Application.Services
{
  public class RoleAppService : IRoleAppService
  {
    private readonly IRoleDomainService? _roleDomainService;
    private readonly IMapper? _mapper;

    public RoleAppService(IRoleDomainService? roleDomainService, IMapper? mapper)
    {
      _roleDomainService = roleDomainService;
      _mapper = mapper;
    }

    public RoleResponseDto Add(RoleAddRequestDto dto)
    {

      try
      {
        var role = new Role
        {
          Id = Guid.NewGuid(),
          RoleName = dto.RoleName,
          CreatedAt = DateTime.Now
        };

        _roleDomainService?.Add(role);
        return _mapper.Map<RoleResponseDto>(role);
      }
      catch (RoleAlreadyExistsException e)
      {
        throw new ApplicationException(e.Message);
      }
      catch (Exception e)
      {
        throw new Exception($"Error: {e.Message}, reporte ao Administrador do sistema o erro informado.");
      }
    }
    public RoleResponseDto Update(Guid id, RoleUpdateRequestDto dto)
    {
      try
      {
        var role = _roleDomainService?.Get(id);

        role.RoleName = dto.RoleName;


        _roleDomainService?.Update(role);

        return _mapper.Map<RoleResponseDto>(role);
      }
      catch (Exception e)
      {
        throw new Exception($"Error: {e.Message}, reporte ao Administrador do sistema o erro informado.");
      }
    }

    public RoleResponseDto Delete(Guid id)
    {
      try
      {
        var role = _roleDomainService?.Get(id);

        _roleDomainService?.Delete(role);

        return _mapper.Map<RoleResponseDto>(role);
      }
      catch (Exception e)
      {
        throw new Exception($"Error: {e.Message}, reporte ao Administrador do sistema o erro informado.");
      }
    }

    public List<RoleResponseDto> GetAll()
    {
      var roles = _roleDomainService?.GetAll().ToList();
      return _mapper.Map<List<RoleResponseDto>>(roles);
    }

    public RoleResponseDto Get(Guid id)
    {
      var role = _roleDomainService?.Get(id);
      return _mapper.Map<RoleResponseDto>(role);
    }

    public void Dispose()
    {
      _roleDomainService.Dispose();
    }
  }
}
