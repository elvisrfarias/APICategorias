using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICategoria.Infra.Context;

public class AppDbContexto : DbContext
{
    public AppDbContexto(DbContextOptions<AppDbContexto> options) : base(options)
    {

    }

    public DbSet<Categoria>? Categorias { get; set; }

    public DbSet<Produto>? Produtos { get; set; }
}
