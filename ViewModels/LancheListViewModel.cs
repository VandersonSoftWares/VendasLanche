using MVCLanche.Models;

namespace MVCLanche.ViewModels
{
    public class LancheListViewModel
    {
        public IEnumerable<Lanche> Lanches { get; set; } = Enumerable.Empty<Lanche>();

        public string? CategoriaAtual { get; set; }
    }
}