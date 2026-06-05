using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario?> GetByEmailAsync(string email);
}
