using Microsoft.EntityFrameworkCore;
using MVCLanche.Context;
using MVCLanche.Models;
using MVCLanche.Repositories.Interfaces;

namespace MVCLanche.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias =>
            _context.Categorias.AsNoTracking();
    }
}