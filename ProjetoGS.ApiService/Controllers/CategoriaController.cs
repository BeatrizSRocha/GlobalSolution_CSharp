using Microsoft.AspNetCore.Mvc;
using ProjetoGS.ApiService.Repositories;

namespace ProjetoGS.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaController(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categorias = await _categoriaRepository.GetAllAsync();
        return Ok(categorias);
    }
}
