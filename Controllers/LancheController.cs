using Microsoft.AspNetCore.Mvc;
using MVCLanche.Models;
using MVCLanche.Repositories.Interfaces;
using MVCLanche.ViewModels;

namespace MVCLanche.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult Index(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                // Filtra dinamicamente por qualquer categoria que venha na URL
                lanches = _lancheRepository.Lanches
                    .Where(l => l.Categoria.CategoriaNome.Equals(categoria, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(l => l.Nome);

                // Define o nome correto ou um aviso caso a categoria não tenha lanches
                categoriaAtual = lanches.Any() ? categoria : "Categoria não encontrada";
            }

            var lanchesListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lanchesListViewModel);
        }
        public IActionResult Details(int lancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
            if (lanche == null)
            {
                return NotFound();
            }
            return View(lanche);
        }
        public ViewResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;
            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches
                    .Where(l => l.Nome.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(l => l.Nome);
                categoriaAtual = lanches.Any() ? $"Lanches contendo \"{searchString}\"" : "Nenhum lanche encontrado";
            }
            return View("~/Views/Lanche/Index.cshtml", new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            });
        }

    }
}
