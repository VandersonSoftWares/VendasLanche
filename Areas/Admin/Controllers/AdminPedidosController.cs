using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCLanche.Repositories.Interfaces;
using MVCLanche.ViewModels;

namespace MVCLanche.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Admin")]
    public class AdminPedidosController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;

        public AdminPedidosController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<IActionResult> Index(int pagina = 1)
        {
            const int tamanhoPagina = 6;

            if (pagina < 1)
                pagina = 1;

            var resultado = await _pedidoRepository
                .GetPedidosPaginadosAsync(pagina, tamanhoPagina);

            var totalPaginas = (int)Math.Ceiling(
                (double)resultado.TotalPedidos / tamanhoPagina);

            if (totalPaginas > 0 && pagina > totalPaginas)
                pagina = totalPaginas;

            var viewModel = new PedidoPaginacaoViewModel
            {
                Pedidos = resultado.Pedidos,
                PaginaAtual = pagina,
                TotalPaginas = totalPaginas,
                TotalPedidos = resultado.TotalPedidos,
                TamanhoPagina = tamanhoPagina
            };

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var pedido = _pedidoRepository.GetPedidoById(id);

            if (pedido == null)
                return NotFound();

            return View(pedido);
        }
    }
}