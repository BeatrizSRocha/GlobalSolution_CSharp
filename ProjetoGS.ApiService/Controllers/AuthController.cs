using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using ProjetoGS.ApiService.Models;
using ProjetoGS.ApiService.Repositories;

namespace ProjetoGS.ApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
        {
            return BadRequest(new { Message = "Email e senha são obrigatórios." });
        }

        var existingUser = await _usuarioRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return BadRequest(new { Message = "Usuário com este e-mail já está cadastrado." });
        }

        // Default role is Pesquisador if invalid or empty
        var role = request.Perfil;
        if (role != "Administrador" && role != "Pesquisador")
        {
            role = "Pesquisador";
        }

        var user = new Usuario
        {
            Nome = request.Nome,
            Email = request.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
            Perfil = role
        };

        await _usuarioRepository.AddAsync(user);
        await _usuarioRepository.SaveChangesAsync();

        return Ok(new UserResponse(user.Id, user.Nome, user.Email, user.Perfil));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
        {
            return BadRequest(new { Message = "Email e senha são obrigatórios." });
        }

        var user = await _usuarioRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            return Unauthorized(new { Message = "E-mail ou senha inválidos." });
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Senha, user.SenhaHash);
        if (!isPasswordValid)
        {
            return Unauthorized(new { Message = "E-mail ou senha inválidos." });
        }

        return Ok(new UserResponse(user.Id, user.Nome, user.Email, user.Perfil));
    }
}

public record RegisterRequest(string Nome, string Email, string Senha, string Perfil);
public record LoginRequest(string Email, string Senha);
public record UserResponse(int Id, string Nome, string Email, string Perfil);
