using MVCLanche.Models;

namespace MVCLanche.ViewModels
{
    public class PedidoPaginacaoViewModel
    {
        public IEnumerable<Pedido> Pedidos { get; set; } = [];

        public int PaginaAtual { get; set; }

        public int TotalPaginas { get; set; }

        public int TotalPedidos { get; set; }

        public int TamanhoPagina { get; set; } = 6;

        public bool TemPaginaAnterior =>
            PaginaAtual > 1;

        public bool TemProximaPagina =>
            PaginaAtual < TotalPaginas;
    }
}