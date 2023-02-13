using MeuMercado.Data.Map;
using MeuMercado.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuMercado.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base (options) 
        {

        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<NotaFiscalModel> NotasFiscais { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProdutoMap());
            builder.ApplyConfiguration(new NotaFiscalMap());
            base.OnModelCreating(builder);
        }
    }
}
