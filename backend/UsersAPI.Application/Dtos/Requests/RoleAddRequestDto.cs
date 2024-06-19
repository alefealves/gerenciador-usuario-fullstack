using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Application.Dtos.Requests
{
  public class RoleAddRequestDto
  {
    [Required(ErrorMessage = "Informe o Nome do Perfil.")]
    [MinLength(2, ErrorMessage = "Informe o Nome do Perfil com pelo menos {1} caracteres.")]
    [MaxLength(50, ErrorMessage = "Informe o Nome do Perfil com no máximo {1} caracteres.")]
    public string? RoleName { get; set; }
  }
}
