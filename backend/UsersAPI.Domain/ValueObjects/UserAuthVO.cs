namespace UsersAPI.Domain.ValueObjects
{
  public class UserAuthVO
  {
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public string? AccessToken { get; set; }
    public DateTime? Expiration { get; set; }
    public DateTime SignedAt { get; set; }
  }
}
