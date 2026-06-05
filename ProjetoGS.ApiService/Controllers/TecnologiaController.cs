using Microsoft.AspNetCore.Mvc;
using ProjetoGS.ApiService.Models;
using ProjetoGS.ApiService.Repositories;

namespace ProjetoGS.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TecnologiaController : ControllerBase
{
    private readonly ITecnologiaRepository _tecnologiaRepository;

    public TecnologiaController(ITecnologiaRepository tecnologiaRepository)
    {
        _tecnologiaRepository = tecnologiaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tecnologias = await _tecnologiaRepository.GetAllWithCategoryAsync();
        return Ok(tecnologias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tecnologia = await _tecnologiaRepository.GetByIdWithCategoryAsync(id);
        if (tecnologia == null)
        {
            return NotFound(new { Message = "Tecnologia não encontrada." });
        }
        return Ok(tecnologia);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Tecnologia tecnologia)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        tecnologia.DataCadastro = DateTime.UtcNow;
        await _tecnologiaRepository.AddAsync(tecnologia);
        await _tecnologiaRepository.SaveChangesAsync();

        // Reload to include Category information in returned value
        var savedTech = await _tecnologiaRepository.GetByIdWithCategoryAsync(tecnologia.Id);
        return CreatedAtAction(nameof(GetById), new { id = tecnologia.Id }, savedTech);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Tecnologia tecnologia)
    {
        if (id != tecnologia.Id)
        {
            return BadRequest(new { Message = "IDs não correspondem." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingTech = await _tecnologiaRepository.GetByIdAsync(id);
        if (existingTech == null)
        {
            return NotFound(new { Message = "Tecnologia não encontrada." });
        }

        // Map values
        existingTech.Nome = tecnologia.Nome;
        existingTech.Descricao = tecnologia.Descricao;
        existingTech.CategoriaImpactoId = tecnologia.CategoriaImpactoId;
        existingTech.MissaoOrigem = tecnologia.MissaoOrigem;
        existingTech.AnoInovacao = tecnologia.AnoInovacao;
        existingTech.BeneficioTerra = tecnologia.BeneficioTerra;

        _tecnologiaRepository.Update(existingTech);
        await _tecnologiaRepository.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var tecnologia = await _tecnologiaRepository.GetByIdAsync(id);
        if (tecnologia == null)
        {
            return NotFound(new { Message = "Tecnologia não encontrada." });
        }

        _tecnologiaRepository.Delete(tecnologia);
        await _tecnologiaRepository.SaveChangesAsync();

        return NoContent();
    }
}
