using Microsoft.EntityFrameworkCore;
using MVCLanche.Context;
using MVCLanche.Models;

namespace MVCLanche.Services
{
    public class GraficoVendasService
    {
        private readonly AppDbContext _context;

        public GraficoVendasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GraficoVenda>> GetVendasPorPeriodoAsync(int dias = 360)
        {
            var dataInicial = DateTime.Now.AddDays(-dias);

            return await (
                from pd in _context.PedidoDetalhes
                join l in _context.Lanches
                    on pd.LancheId equals l.LancheId
                where pd.Pedido.PedidoEnviado >= dataInicial
                group pd by new
                {
                    pd.LancheId,
                    l.Nome
                } into g
                orderby g.Sum(x => x.Quantidade) descending
                select new GraficoVenda
                {
                    Descricao = g.Key.Nome,
                    Quantidade = g.Sum(x => x.Quantidade),
                    ValorTotal = g.Sum(x => x.Quantidade * x.Preco)
                }
            ).ToListAsync();
        }

        public async Task<List<GraficoVenda>> GetProdutosMaisVendidosAsync()
        {
            return await _context.PedidoDetalhes
                .GroupBy(pd => pd.Lanche.Nome)
                .Select(g => new GraficoVenda
                {
                    Descricao = g.Key,
                    Quantidade = g.Sum(x => x.Quantidade),
                    ValorTotal = g.Sum(x => x.Quantidade * x.Preco)
                })
                .OrderByDescending(x => x.Quantidade)
                .ToListAsync();
        }

        public async Task<List<GraficoVenda>> GetFaturamentoMensalAsync()
        {
            var dataInicial = DateTime.Now.AddMonths(-12);

            var dados = await _context.PedidoDetalhes
                .Where(pd => pd.Pedido.PedidoEnviado >= dataInicial)
                .Select(pd => new
                {
                    Ano = pd.Pedido.PedidoEnviado.Year,
                    Mes = pd.Pedido.PedidoEnviado.Month,
                    Valor = pd.Quantidade * pd.Preco
                })
                .ToListAsync();

            var resultado = dados
                .GroupBy(x => new { x.Ano, x.Mes })
                .OrderBy(g => g.Key.Ano)
                .ThenBy(g => g.Key.Mes)
                .Select(g => new GraficoVenda
                {
                    Descricao = g.Key.Mes.ToString("00") + "/" + g.Key.Ano.ToString(),
                    ValorTotal = g.Sum(x => x.Valor)
                })
                .ToList();

            return resultado;
        }


    }
}