using MVCLanche.Models;

namespace MVCLanche.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);

        IEnumerable<Pedido> Pedidos { get; }

        Pedido? GetPedidoById(int pedidoId);

        Task<(IEnumerable<Pedido> Pedidos, int TotalPedidos)>
            GetPedidosPaginadosAsync(int pagina, int tamanhoPagina);
    }
}