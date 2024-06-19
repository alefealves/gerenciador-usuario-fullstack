using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Application.Dtos.Requests
{
  public class SubModuleAddRequestDto
  {
    [Required(ErrorMessage = "Informe o ID do Módulo.")]
    public Guid ModuleId { get; set; }

    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid ParentSubModuleId { get; set; }

    [Required(ErrorMessage = "Informe o Nome do SubMódulo.")]
    [MinLength(2, ErrorMessage = "Informe o Nome do SubMódulo com pelo menos {1} caracteres.")]
    [MaxLength(50, ErrorMessage = "Informe o Nome do SubMódulo com no máximo {1} caracteres.")]
    public string? SubModuleName { get; set; }
  }
}
