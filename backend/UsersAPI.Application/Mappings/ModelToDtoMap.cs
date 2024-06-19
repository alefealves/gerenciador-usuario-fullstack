using AutoMapper;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Domain.Entities;

namespace UsersAPI.Application.Mappings
{
  public class ModelToDtoMap : Profile
  {
    public ModelToDtoMap()
    {

      CreateMap<Module, ModuleResponseDto>();

      CreateMap<Role, RoleResponseDto>();

      CreateMap<Permission, PermissionResponseDto>()
          .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
          .ForMember(dest => dest.SubModuleName, opt => opt.MapFrom(src => src.SubModule.SubModuleName));

      CreateMap<SubModule, SubModuleResponseDto>()
          .ForMember(dest => dest.ModuleName, opt => opt.MapFrom(src => src.Module.ModuleName));

      CreateMap<User, UserResponseDto>()
          .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));
    }
  }
}
