using Microsoft.EntityFrameworkCore;
using ProjetoGS.ApiService.Models;

namespace ProjetoGS.ApiService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<CategoriaImpacto> CategoriasImpacto { get; set; }
    public DbSet<Tecnologia> Tecnologias { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Enforce unique user email
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Specific configuration if needed
        modelBuilder.Entity<Tecnologia>()
            .HasOne(t => t.CategoriaImpacto)
            .WithMany(c => c.Tecnologias)
            .HasForeignKey(t => t.CategoriaImpactoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
