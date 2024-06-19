using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Application.Dtos.Requests
{
  public class ResetPasswordRequestDto
  {
    [Required(ErrorMessage = "Informe a senha de acesso atual.")]
    public string? CurrentPassword { get; set; }

    [Required(ErrorMessage = "Informe a nova senha de acesso.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+=])(?!.*\s).{8,}$",
        ErrorMessage = "Informe uma senha forte com pelo menos 8 caracteres.")]
    public string? NewPassword { get; set; }

    [Required(ErrorMessage = "Confirme a nova senha de acesso.")]
    [Compare("NewPassword", ErrorMessage = "Senhas não conferem.")]
    public string? NewPasswordConfirm { get; set; }
  }
}
