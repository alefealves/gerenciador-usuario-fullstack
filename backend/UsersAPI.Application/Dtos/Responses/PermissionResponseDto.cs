namespace UsersAPI.Application.Dtos.Responses
{
  public class PermissionResponseDto
  {
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public string? RoleName { get; set; }
    public Guid SubModuleId { get; set; }
    public string? SubModuleName { get; set; }
    public bool Create { get; set; }
    public bool Update { get; set; }
    public bool Delete { get; set; }
    public bool Read { get; set; }
  }
}
