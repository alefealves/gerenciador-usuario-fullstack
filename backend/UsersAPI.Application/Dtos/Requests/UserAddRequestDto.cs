using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Application.Dtos.Requests
{
  public class UserAddRequestDto
  {
    [Required(ErrorMessage = "Informe um Perfil.")]
    public Guid RoleId { get; set; }

    [Required(ErrorMessage = "Informe o Nome.")]
    [MinLength(2, ErrorMessage = "Informe o Nome com pelo menos {1} caracteres.")]
    [MaxLength(50, ErrorMessage = "Informe o Nome com no máximo {1} caracteres.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Informe o Sobrenome.")]
    [MinLength(2, ErrorMessage = "Informe o Sobrenome com pelo menos {1} caracteres.")]
    [MaxLength(50, ErrorMessage = "Informe o Sobrenome com no máximo {1} caracteres.")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Informe o email de acesso.")]
    [EmailAddress(ErrorMessage = "Informe um email válido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Informe a senha de acesso.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "Informe uma senha forte com pelo menos 8 caracteres.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Confirme a senha de acesso.")]
    [Compare("Password", ErrorMessage = "Senhas não conferem.")]
    public string? PasswordConfirm { get; set; }

  }
}
