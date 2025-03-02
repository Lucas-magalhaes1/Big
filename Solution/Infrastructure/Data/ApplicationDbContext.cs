using Big.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Big.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        
        public DbSet<Cliente> Clientes { get; set; }
        
        public DbSet<Pedido> Pedidos { get; set; }
        
        public DbSet<ProdutoPedido> ProdutosPedidos { get; set; }
        
        
    }
}