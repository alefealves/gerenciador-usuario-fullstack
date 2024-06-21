namespace UsersAPI.Application.Dtos.Responses
{
  public class SubModuleResponseDto
  {
    public Guid Id { get; set; }
    public Guid ParentSubModuleId { get; set; }
    public string? SubModuleName { get; set; }
    public Guid ModuleId { get; set; }

    public string? ModuleName { get; set; }
  }
}
