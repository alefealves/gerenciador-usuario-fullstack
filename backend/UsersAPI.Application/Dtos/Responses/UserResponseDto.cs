using UsersAPI.Domain.Entities;

namespace UsersAPI.Application.Dtos.Responses
{
  public class UserResponseDto
  {
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public string? RoleName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public bool Active { get; set; }
    public bool EmailConfirmed { get; set; }
  }
}
