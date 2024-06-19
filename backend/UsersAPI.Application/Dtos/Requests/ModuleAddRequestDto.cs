using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Application.Dtos.Requests
{
  public class ModuleAddRequestDto
  {
    [Required(ErrorMessage = "Informe o Nome do Módulo.")]
    [MinLength(2, ErrorMessage = "Informe o Nome do Módulo com pelo menos {1} caracteres.")]
    [MaxLength(50, ErrorMessage = "Informe o Nome do Módulo com no máximo {1} caracteres.")]
    public string? ModuleName { get; set; }
  }
}
