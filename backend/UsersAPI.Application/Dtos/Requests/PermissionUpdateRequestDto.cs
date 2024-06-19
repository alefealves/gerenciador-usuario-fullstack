namespace UsersAPI.Application.Dtos.Requests
{
  public class PermissionUpdateRequestDto
  {
    public bool Create { get; set; }
    public bool Update { get; set; }
    public bool Delete { get; set; }
    public bool Read { get; set; }
  }
}
