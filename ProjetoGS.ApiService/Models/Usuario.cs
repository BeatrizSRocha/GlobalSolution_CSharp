using System.ComponentModel.DataAnnotations;

namespace ProjetoGS.ApiService.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string SenhaHash { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Perfil { get; set; } = string.Empty; // e.g. "Administrador", "Pesquisador"
}
