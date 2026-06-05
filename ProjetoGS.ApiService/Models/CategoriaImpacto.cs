using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoGS.ApiService.Models;

public class CategoriaImpacto
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Descricao { get; set; } = string.Empty;

    // Navigation property (ignored in JSON to avoid cycle reference)
    [JsonIgnore]
    public ICollection<Tecnologia> Tecnologias { get; set; } = new List<Tecnologia>();
}
