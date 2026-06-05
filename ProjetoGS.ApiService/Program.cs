using Microsoft.EntityFrameworkCore;
using ProjetoGS.ApiService.Data;
using ProjetoGS.ApiService.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Configure EF Core MySQL DbContext
var connectionString = builder.Configuration.GetConnectionString("gsdb") 
    ?? "Server=localhost;Database=nova_economia_espacial;Uid=root;Pwd=root;";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 30))));

// Register Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ITecnologiaRepository, TecnologiaRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Add Controllers
builder.Services.AddControllers();

// Add service defaults and problem details
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Automatically create database and seed default values
using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureCreated();
        DbInitializer.Initialize(dbContext);
    }
    catch (Exception ex)
    {
        // Log error (or output to console during startup)
        Console.WriteLine($"Erro ao inicializar banco de dados: {ex.Message}");
    }
}

app.MapGet("/", () => "API service is running. Use /api/tecnologia or /api/tecnologias/stats.");

app.MapControllers();
app.MapDefaultEndpoints();

app.Run();
