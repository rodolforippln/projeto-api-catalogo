using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Data
{
    public class CatalogoContex : DbContext
    {
        public CatalogoContex(DbContextOptions<CatalogoContex> options) : base( options )
        {}

        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Produto> Produtos => Set<Produto>();
    }
}
