using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;

namespace Biblioteca.Data
{
    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Libro> Libri { get; set; }
        public DbSet<Prestito> Prestiti { get; set; }

    }
}
