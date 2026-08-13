// Repositories/LancheRepository.cs
using MVCLanche.Models;
using MVCLanche.Context;
using MVCLanche.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class LancheRepository : ILancheRepository
{
    private readonly AppDbContext _context;

    public LancheRepository(AppDbContext context)
    {
        _context = context;
    }

    // Retorna todos os lanches incluindo a sua categoria associada (Eager Loading)
    public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);

    public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches
        .Where(l => l.IsLanchePreferido)
        .Include(c => c.Categoria);

    public Lanche GetLancheById(int lancheId) => _context.Lanches
        .FirstOrDefault(l => l.LancheId == lancheId);
}
