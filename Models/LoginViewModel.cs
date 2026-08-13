using System.ComponentModel.DataAnnotations;

namespace MVCLanche.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "O nome de usuário é obrigatório")]
    [Display(Name = "Usuário")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "A Senha é obrigatória")]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Password { get; set; } = string.Empty;

    public string? ReturnUrl { get; set; }
}