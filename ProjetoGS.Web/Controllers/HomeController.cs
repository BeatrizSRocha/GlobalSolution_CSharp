using Microsoft.AspNetCore.Mvc;
using ProjetoGS.Web.Models;
using ProjetoGS.Web.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProjetoGS.Web.Controllers;

public class HomeController : Controller
{
    private readonly SpaceTechApiClient _apiClient;
    private readonly ILogger<HomeController> _logger;

    public HomeController(SpaceTechApiClient apiClient, ILogger<HomeController> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var stats = await _apiClient.GetStatsAsync();
            var tecnologias = await _apiClient.GetTecnologiasAsync();

            // Keep latest 5 items for the dashboard table
            var latestTecnologias = tecnologias.Take(5).ToList();

            ViewBag.Stats = stats;
            return View(latestTecnologias);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao carregar dados da página inicial.");
            ViewBag.Stats = new StatsViewModel();
            return View(new List<TecnologiaViewModel>());
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
