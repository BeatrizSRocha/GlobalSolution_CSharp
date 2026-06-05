using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoGS.Web.Models;

public class TecnologiaViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da tecnologia é obrigatório.")]
    [StringLength(150, ErrorMessage = "O nome não pode exceder 150 caracteres.")]
    [Display(Name = "Nome da Tecnologia")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(2000, ErrorMessage = "A descrição não pode exceder 2000 caracteres.")]
    [Display(Name = "Descrição da Tecnologia")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "A categoria de impacto é obrigatória.")]
    [Display(Name = "Categoria de Impacto")]
    public int CategoriaImpactoId { get; set; }

    public CategoriaImpactoViewModel? CategoriaImpacto { get; set; }

    [Required(ErrorMessage = "A missão de origem é obrigatória.")]
    [StringLength(150, ErrorMessage = "A missão de origem não pode exceder 150 caracteres.")]
    [Display(Name = "Missão de Origem (Ex: Apollo 11)")]
    public string MissaoOrigem { get; set; } = string.Empty;

    [Required(ErrorMessage = "O ano de inovação é obrigatório.")]
    [Range(1900, 2100, ErrorMessage = "Insira um ano válido entre 1900 e 2100.")]
    [Display(Name = "Ano da Inovação")]
    public int AnoInovacao { get; set; }

    [Required(ErrorMessage = "O benefício para a Terra é obrigatório.")]
    [StringLength(2000, ErrorMessage = "O benefício para a Terra não pode exceder 2000 caracteres.")]
    [Display(Name = "Impacto / Benefício na Terra")]
    public string BeneficioTerra { get; set; } = string.Empty;

    [Display(Name = "Data de Cadastro")]
    public DateTime DataCadastro { get; set; }
}
