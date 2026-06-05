using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ProjetoGS.Web.Models;

namespace ProjetoGS.Web.Services;

public class SpaceTechApiClient
{
    private readonly HttpClient _client;

    public SpaceTechApiClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<TecnologiaViewModel>> GetTecnologiasAsync()
    {
        try
        {
            return await _client.GetFromJsonAsync<IEnumerable<TecnologiaViewModel>>("api/tecnologia") 
                ?? Array.Empty<TecnologiaViewModel>();
        }
        catch (Exception)
        {
            return Array.Empty<TecnologiaViewModel>();
        }
    }

    public async Task<TecnologiaViewModel?> GetTecnologiaByIdAsync(int id)
    {
        try
        {
            return await _client.GetFromJsonAsync<TecnologiaViewModel>($"api/tecnologia/{id}");
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> CreateTecnologiaAsync(TecnologiaViewModel model)
    {
        try
        {
            var response = await _client.PostAsJsonAsync("api/tecnologia", model);
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateTecnologiaAsync(int id, TecnologiaViewModel model)
    {
        try
        {
            var response = await _client.PutAsJsonAsync($"api/tecnologia/{id}", model);
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteTecnologiaAsync(int id)
    {
        try
        {
            var response = await _client.DeleteAsync($"api/tecnologia/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<IEnumerable<CategoriaImpactoViewModel>> GetCategoriasAsync()
    {
        try
        {
            return await _client.GetFromJsonAsync<IEnumerable<CategoriaImpactoViewModel>>("api/categoria") 
                ?? Array.Empty<CategoriaImpactoViewModel>();
        }
        catch (Exception)
        {
            return Array.Empty<CategoriaImpactoViewModel>();
        }
    }

    public async Task<StatsViewModel> GetStatsAsync()
    {
        try
        {
            return await _client.GetFromJsonAsync<StatsViewModel>("api/tecnologias/stats") 
                ?? new StatsViewModel();
        }
        catch (Exception)
        {
            return new StatsViewModel();
        }
    }

    public async Task<UserResponseViewModel?> LoginAsync(LoginViewModel model)
    {
        try
        {
            var response = await _client.PostAsJsonAsync("api/auth/login", model);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserResponseViewModel>();
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<UserResponseViewModel?> RegisterAsync(RegisterViewModel model)
    {
        try
        {
            var response = await _client.PostAsJsonAsync("api/auth/register", model);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserResponseViewModel>();
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}

public record UserResponseViewModel(int Id, string Nome, string Email, string Perfil);
