using AutoMapper;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Application.Interfaces.Application;
using UsersAPI.Domain.Entities;
using UsersAPI.Domain.Exceptions;
using UsersAPI.Domain.Interfaces.Services;

namespace UsersAPI.Application.Services
{
  public class ModuleAppService : IModuleAppService
  {
    private readonly IModuleDomainService? _moduleDomainService;
    private readonly IMapper? _mapper;

    public ModuleAppService(IModuleDomainService? moduleDomainService, IMapper? mapper)
    {
      _moduleDomainService = moduleDomainService;
      _mapper = mapper;
    }

    public ModuleResponseDto Add(ModuleAddRequestDto dto)
    {
      try
      {
        var module = new Module
        {
          Id = Guid.NewGuid(),
          ModuleName = dto.ModuleName,
          CreatedAt = DateTime.Now
        };

        _moduleDomainService?.Add(module);
        return _mapper.Map<ModuleResponseDto>(module);
      }
      catch (ModuleAlreadyExistsException e)
      {
        throw new ApplicationException(e.Message);
      }
      catch (Exception e)
      {
        throw new Exception($"Error: {e.Message}, reporte ao Administrador do sistema o erro informado.");
      }

    }

    public ModuleResponseDto Update(Guid id, ModuleUpdateRequestDto dto)
    {
      try
      {
        var module = _moduleDomainService?.Get(id);

        module.ModuleName = dto.ModuleName;

        _moduleDomainService?.Update(module);

        return _mapper.Map<ModuleResponseDto>(module);
      }
      catch (Exception e)
      {
        throw new Exception($"Error: {e.Message}, reporte ao Administrador do sistema o erro informado.");
      }
    }

    public ModuleResponseDto Delete(Guid id)
    {
      try
      {
        var module = _moduleDomainService?.Get(id);

        _moduleDomainService?.Delete(module);

        return _mapper.Map<ModuleResponseDto>(module);
      }
      catch (Exception e)
      {
        throw new Exception($"Error: {e.Message}, reporte ao Administrador do sistema o erro informado.");
      }
    }

    public List<ModuleResponseDto> GetAll()
    {
      var modules = _moduleDomainService?.GetAll().ToList();
      return _mapper.Map<List<ModuleResponseDto>>(modules);
    }

    public ModuleResponseDto Get(Guid id)
    {
      var module = _moduleDomainService?.Get(id);
      return _mapper.Map<ModuleResponseDto>(module);
    }

    public void Dispose()
    {
      _moduleDomainService?.Dispose();
    }
  }
}
