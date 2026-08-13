using MVCLanche.Models;

namespace MVCLanche.ViewModels
{
    public class DashboardGraficoViewModel
    {
        public List<GraficoVenda> Vendas30Dias { get; set; } = [];

        public List<GraficoVenda> Vendas360Dias { get; set; } = [];

        public List<GraficoVenda> ProdutosMaisVendidos { get; set; } = [];

        public List<GraficoVenda> FaturamentoMensal { get; set; } = [];
    }
}