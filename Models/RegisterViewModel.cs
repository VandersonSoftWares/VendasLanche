using System.ComponentModel.DataAnnotations;

namespace MVCLanche.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "O nome de usuário é obrigatório")]
    [Display(Name = "Utilizador")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "A Senha é obrigatória")]
    [DataType(DataType.Password)]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "A {0} deve ter entre {2} e {1} caracteres.")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirmar Password")]
    [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
    public string ConfirmPassword { get; set; }
}
