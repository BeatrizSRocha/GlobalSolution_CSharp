using Microsoft.EntityFrameworkCore;
using ProjetoGS.ApiService.Data;
using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Repositories;

public class TecnologiaRepository : Repository<Tecnologia>, ITecnologiaRepository
{
    public TecnologiaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Tecnologia>> GetAllWithCategoryAsync()
    {
        return await _context.Tecnologias
            .Include(t => t.CategoriaImpacto)
            .OrderByDescending(t => t.Id)
            .ToListAsync();
    }

    public async Task<Tecnologia?> GetByIdWithCategoryAsync(int id)
    {
        return await _context.Tecnologias
            .Include(t => t.CategoriaImpacto)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}
