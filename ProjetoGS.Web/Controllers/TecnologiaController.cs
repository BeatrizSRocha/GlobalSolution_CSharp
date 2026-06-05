using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjetoGS.Web.Models;
using ProjetoGS.Web.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoGS.Web.Controllers;

public class TecnologiaController : Controller
{
    private readonly SpaceTechApiClient _apiClient;

    public TecnologiaController(SpaceTechApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    // GET: Tecnologia
    public async Task<IActionResult> Index(string? search = null, int? categoryId = null)
    {
        var tecnologias = await _apiClient.GetTecnologiasAsync();
        var categorias = await _apiClient.GetCategoriasAsync();

        // Apply text filter (matches Name or Space Mission)
        if (!string.IsNullOrWhiteSpace(search))
        {
            tecnologias = tecnologias.Where(t => 
                t.Nome.Contains(search, StringComparison.OrdinalIgnoreCase) || 
                t.MissaoOrigem.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                t.BeneficioTerra.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        // Apply category filter
        if (categoryId.HasValue)
        {
            tecnologias = tecnologias.Where(t => t.CategoriaImpactoId == categoryId.Value);
        }

        ViewBag.Categorias = new SelectList(categorias, "Id", "Nome", categoryId);
        ViewBag.Search = search;
        ViewBag.SelectedCategory = categoryId;

        return View(tecnologias);
    }

    // GET: Tecnologia/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var tecnologia = await _apiClient.GetTecnologiaByIdAsync(id);
        if (tecnologia == null)
        {
            return NotFound();
        }
        return View(tecnologia);
    }

    // GET: Tecnologia/Create
    [Authorize(Roles = "Administrador,Pesquisador")]
    public async Task<IActionResult> Create()
    {
        var categorias = await _apiClient.GetCategoriasAsync();
        ViewBag.CategoriaImpactoId = new SelectList(categorias, "Id", "Nome");
        return View();
    }

    // POST: Tecnologia/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador,Pesquisador")]
    public async Task<IActionResult> Create(TecnologiaViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var categorias = await _apiClient.GetCategoriasAsync();
            ViewBag.CategoriaImpactoId = new SelectList(categorias, "Id", "Nome", model.CategoriaImpactoId);
            return View(model);
        }

        var success = await _apiClient.CreateTecnologiaAsync(model);
        if (success)
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, "Não foi possível cadastrar a tecnologia. Verifique se o servidor de API está ativo.");
        var cats = await _apiClient.GetCategoriasAsync();
        ViewBag.CategoriaImpactoId = new SelectList(cats, "Id", "Nome", model.CategoriaImpactoId);
        return View(model);
    }

    // GET: Tecnologia/Edit/5
    [Authorize(Roles = "Administrador,Pesquisador")]
    public async Task<IActionResult> Edit(int id)
    {
        var tecnologia = await _apiClient.GetTecnologiaByIdAsync(id);
        if (tecnologia == null)
        {
            return NotFound();
        }

        var categorias = await _apiClient.GetCategoriasAsync();
        ViewBag.CategoriaImpactoId = new SelectList(categorias, "Id", "Nome", tecnologia.CategoriaImpactoId);
        return View(tecnologia);
    }

    // POST: Tecnologia/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador,Pesquisador")]
    public async Task<IActionResult> Edit(int id, TecnologiaViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            var categorias = await _apiClient.GetCategoriasAsync();
            ViewBag.CategoriaImpactoId = new SelectList(categorias, "Id", "Nome", model.CategoriaImpactoId);
            return View(model);
        }

        var success = await _apiClient.UpdateTecnologiaAsync(id, model);
        if (success)
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, "Não foi possível atualizar os dados da tecnologia.");
        var cats = await _apiClient.GetCategoriasAsync();
        ViewBag.CategoriaImpactoId = new SelectList(cats, "Id", "Nome", model.CategoriaImpactoId);
        return View(model);
    }

    // GET: Tecnologia/Delete/5
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(int id)
    {
        var tecnologia = await _apiClient.GetTecnologiaByIdAsync(id);
        if (tecnologia == null)
        {
            return NotFound();
        }
        return View(tecnologia);
    }

    // POST: Tecnologia/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var success = await _apiClient.DeleteTecnologiaAsync(id);
        if (success)
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, "Falha ao excluir a tecnologia.");
        var tecnologia = await _apiClient.GetTecnologiaByIdAsync(id);
        return View(tecnologia);
    }
}
