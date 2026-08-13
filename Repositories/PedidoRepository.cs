using Microsoft.EntityFrameworkCore;
using MVCLanche.Context;
using MVCLanche.Models;
using MVCLanche.Repositories.Interfaces;

namespace MVCLanche.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext appDbContext, CarrinhoCompra carrinhoCompra)
        {
            _appDbContext = appDbContext;
            _carrinhoCompra = carrinhoCompra;
        }

        public IEnumerable<Pedido> Pedidos =>
            _appDbContext.Pedidos
                .Include(p => p.PedidoItens)
                    .ThenInclude(pi => pi.Lanche)
                .OrderByDescending(p => p.PedidoEnviado)
                .ThenByDescending(p => p.PedidoId);

        public Pedido? GetPedidoById(int pedidoId)
        {
            return _appDbContext.Pedidos
                .Include(p => p.PedidoItens)
                    .ThenInclude(pi => pi.Lanche)
                .FirstOrDefault(p => p.PedidoId == pedidoId);
        }

        public async Task<(IEnumerable<Pedido> Pedidos, int TotalPedidos)>
            GetPedidosPaginadosAsync(int pagina, int tamanhoPagina)
        {
            var query = _appDbContext.Pedidos
                .Include(p => p.PedidoItens)
                    .ThenInclude(pi => pi.Lanche)
                .OrderByDescending(p => p.PedidoId);

            var totalPedidos = await query.CountAsync();

            var pedidos = await query
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            return (pedidos, totalPedidos);
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;

            _appDbContext.Pedidos.Add(pedido);
            _appDbContext.SaveChanges();

            foreach (var carrinhoItem in _carrinhoCompra.CarrinhoCompraItems)
            {
                var pedidoDetail = new PedidoDetalhe
                {
                    LancheId = carrinhoItem.Lanche.LancheId,
                    PedidoId = pedido.PedidoId,
                    Quantidade = carrinhoItem.Quantidade,
                    Preco = carrinhoItem.Lanche.Preco
                };

                _appDbContext.PedidoDetalhes.Add(pedidoDetail);
            }

            _appDbContext.SaveChanges();
        }
    }
}