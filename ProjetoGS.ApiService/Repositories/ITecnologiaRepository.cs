using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Repositories;

public interface ITecnologiaRepository : IRepository<Tecnologia>
{
    Task<IEnumerable<Tecnologia>> GetAllWithCategoryAsync();
    Task<Tecnologia?> GetByIdWithCategoryAsync(int id);
}
