using System.ComponentModel.DataAnnotations;

namespace ProjetoGS.Web.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Insira um e-mail válido.")]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Senha { get; set; } = string.Empty;
}

public class RegisterViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
    [Display(Name = "Nome Completo")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Insira um e-mail válido.")]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres.")]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Senha { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Confirmar Senha")]
    [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
    public string ConfirmarSenha { get; set; } = string.Empty;

    [Required(ErrorMessage = "Escolha um perfil de acesso.")]
    [Display(Name = "Perfil de Acesso")]
    public string Perfil { get; set; } = "Pesquisador"; // Default
}
