using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoGS.ApiService.Models;

public class Tecnologia
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(2000)]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public int CategoriaImpactoId { get; set; }

    [ForeignKey("CategoriaImpactoId")]
    public CategoriaImpacto? CategoriaImpacto { get; set; }

    [Required]
    [MaxLength(150)]
    public string MissaoOrigem { get; set; } = string.Empty;

    [Required]
    public int AnoInovacao { get; set; }

    [Required]
    [MaxLength(2000)]
    public string BeneficioTerra { get; set; } = string.Empty;

    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
}
