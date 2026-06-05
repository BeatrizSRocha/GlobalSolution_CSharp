using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoGS.ApiService.Data;
using System.Linq;

namespace ProjetoGS.ApiService.Controllers;

[ApiController]
public class StatsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StatsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("api/tecnologias/stats")]
    public async Task<IActionResult> GetStats()
    {
        var totalTecnologias = await _context.Tecnologias.CountAsync();
        
        var totalMissoes = await _context.Tecnologias
            .Select(t => t.MissaoOrigem.Trim().ToLower())
            .Distinct()
            .CountAsync();
            
        var totalSetores = await _context.CategoriasImpacto.CountAsync();

        var categories = await _context.CategoriasImpacto
            .Select(c => new
            {
                c.Nome,
                Count = c.Tecnologias.Count()
            })
            .ToListAsync();

        var distribuicao = categories.Select(c => new SetorStatsDto
        {
            Setor = c.Nome,
            Quantidade = c.Count,
            Percentual = totalTecnologias > 0 ? Math.Round((double)c.Count * 100 / totalTecnologias, 1) : 0
        }).ToList();

        var stats = new StatsDto
        {
            TotalTecnologias = totalTecnologias,
            TotalMissoes = totalMissoes,
            TotalSetores = totalSetores,
            DistribuicaoSetores = distribuicao
        };

        return Ok(stats);
    }
}

public class StatsDto
{
    public int TotalTecnologias { get; set; }
    public int TotalMissoes { get; set; }
    public int TotalSetores { get; set; }
    public List<SetorStatsDto> DistribuicaoSetores { get; set; } = new();
}

public class SetorStatsDto
{
    public string Setor { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public double Percentual { get; set; }
}
