using ProjetoGS.ApiService.Data;
using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Repositories;

public class CategoriaRepository : Repository<CategoriaImpacto>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }
}
