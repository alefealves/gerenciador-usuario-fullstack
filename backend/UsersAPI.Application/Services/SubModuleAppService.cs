using AutoMapper;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Application.Interfaces.Application;
using UsersAPI.Domain.Entities;
using UsersAPI.Domain.Exceptions;
using UsersAPI.Domain.Interfaces.Services;

namespace UsersAPI.Application.Services
{
  public class SubModuleAppService : ISubModuleAppService
  {
    private readonly ISubModuleDomainService? _subModuleDomainService;
    private readonly IMapper? _mapper;

    public SubModuleAppService(ISubModuleDomainService? subModuleDomainService, IMapper? mapper)
    {
      _subModuleDomainService = subModuleDomainService;
      _mapper = mapper;
    }

    public SubModuleResponseDto Add(SubModuleAddRequestDto dto)
    {
      try
      {
        var subModule = new SubModule
        {
          Id = Guid.NewGuid(),
          ModuleId = dto.ModuleId,
          ParentSubModuleId = dto.ParentSubModuleId,
          SubModuleName = dto.SubModuleName,
          CreatedAt = DateTime.Now
        };

        _subModuleDomainService?.Add(subModule);
        return _mapper.Map<SubModuleResponseDto>(subModule);
      }
      catch (SubModuleAlreadyExistsException e)
      {
        throw new ApplicationException(e.Message);
      }
      catch (ModuleIsNotExistsException e)
      {
        throw new ApplicationException(e.Message);
      }
      catch (Exception e)
      {
        throw new Exception($"Error: {e.Message}, reporte ao Administrador do sistema o erro informado.");
      }
    }

    public SubModuleResponseDto Update(Guid id, SubModuleUpdateRequestDto dto)
    {
      try
      {
        var subModule = _subModuleDomainService?.GetById(id);
        subModule.SubModuleName = dto.SubModuleName;


        _subModuleDomainService?.Update(subModule);

        return _mapper.Map<SubModuleResponseDto>(subModule);
      }
      catch (Exception e)
      {
        throw new Exception($"Error: {e.Message}, reporte ao Administrador do sistema o erro informado.");
      }
    }

    public SubModuleResponseDto Delete(Guid id)
    {
      try
      {
        var subModule = _subModuleDomainService?.GetById(id);

        _subModuleDomainService?.Delete(subModule);

        return _mapper.Map<SubModuleResponseDto>(subModule);
      }
      catch (SubModuleIsNotDeleteException e)
      {
        throw new ApplicationException(e.Message);
      }
      catch (Exception e)
      {
        throw new Exception($"Error: {e.Message}, reporte ao Administrador do sistema o erro informado.");
      }
    }

    public List<SubModuleResponseDto> GetAll()
    {
      var subModules = _subModuleDomainService?.GetAll().ToList();
      return _mapper.Map<List<SubModuleResponseDto>>(subModules);
    }

    public List<SubModuleResponseDto> GetAll(Guid moduleId)
    {
      var subModules = _subModuleDomainService?.GetAll(moduleId).ToList();
      return _mapper.Map<List<SubModuleResponseDto>>(subModules);
    }

    public SubModuleResponseDto Get(Guid id)
    {
      var subModule = _subModuleDomainService?.GetById(id);
      return _mapper.Map<SubModuleResponseDto>(subModule);
    }

    public SubModuleResponseDto GetByModuleId(Guid moduleId)
    {
      var subModule = _subModuleDomainService?.GetByModuleId(moduleId);
      return _mapper.Map<SubModuleResponseDto>(subModule);
    }

    public void Dispose()
    {
      _subModuleDomainService?.Dispose();
    }
  }
}
