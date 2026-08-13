using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCLanche.Context;
using MVCLanche.Services;
using MVCLanche.ViewModels;

namespace MVCLanche.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class AdminGraficoController : Controller
    {
        private readonly GraficoVendasService _graficoVendas;
        private readonly AppDbContext _context;

        public AdminGraficoController(
            GraficoVendasService graficoVendas,
            AppDbContext context)
        {
            _graficoVendas = graficoVendas;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardGraficoViewModel
            {
                Vendas30Dias = await _graficoVendas.GetVendasPorPeriodoAsync(30),

                Vendas360Dias = await _graficoVendas.GetVendasPorPeriodoAsync(360),

                ProdutosMaisVendidos = await _graficoVendas.GetProdutosMaisVendidosAsync(),

                FaturamentoMensal = await _graficoVendas.GetFaturamentoMensalAsync()
            };

            return View(viewModel);
        }
        //Remover daqui para baixo
        public async Task<IActionResult> ExportarPedidosExcel()
        {
            var pedidos = await _context.Pedidos
                .OrderBy(p => p.PedidoId)
                .ToListAsync();

            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Pedidos");


            // =========================================================
            // TÍTULO
            // =========================================================

            worksheet.Range("A1:L1").Merge();

            worksheet.Cell("A1").Value = "RELATÓRIO DE PEDIDOS";

            worksheet.Cell("A1").Style.Font.Bold = true;
            worksheet.Cell("A1").Style.Font.FontSize = 18;

            worksheet.Cell("A1").Style.Alignment.Horizontal =
                XLAlignmentHorizontalValues.Center;

            worksheet.Cell("A1").Style.Alignment.Vertical =
                XLAlignmentVerticalValues.Center;

            worksheet.Row(1).Height = 30;


            // =========================================================
            // CABEÇALHO DA TABELA
            // =========================================================

            int linhaCabecalho = 3;

            worksheet.Cell(linhaCabecalho, 1).Value = "Pedido";
            worksheet.Cell(linhaCabecalho, 2).Value = "Data do Pedido";
            worksheet.Cell(linhaCabecalho, 3).Value = "Cliente";
            worksheet.Cell(linhaCabecalho, 4).Value = "E-mail";
            worksheet.Cell(linhaCabecalho, 5).Value = "Telefone";
            worksheet.Cell(linhaCabecalho, 6).Value = "Endereço";
            worksheet.Cell(linhaCabecalho, 7).Value = "Complemento";
            worksheet.Cell(linhaCabecalho, 8).Value = "CEP";
            worksheet.Cell(linhaCabecalho, 9).Value = "Cidade";
            worksheet.Cell(linhaCabecalho, 10).Value = "Estado";
            worksheet.Cell(linhaCabecalho, 11).Value = "Total de Itens";
            worksheet.Cell(linhaCabecalho, 12).Value = "Total do Pedido";


            // =========================================================
            // DADOS DOS PEDIDOS
            // =========================================================

            var linha = linhaCabecalho + 1;

            foreach (var pedido in pedidos)
            {
                worksheet.Cell(linha, 1).Value =
                    pedido.PedidoId;

                worksheet.Cell(linha, 2).Value =
                    pedido.PedidoEnviado;

                worksheet.Cell(linha, 3).Value =
                    $"{pedido.Nome} {pedido.Sobrenome}";

                worksheet.Cell(linha, 4).Value =
                    pedido.Email;

                worksheet.Cell(linha, 5).Value =
                    pedido.Telefone;

                worksheet.Cell(linha, 6).Value =
                    pedido.Endereco1;

                worksheet.Cell(linha, 7).Value =
                    pedido.Endereco2;

                worksheet.Cell(linha, 8).Value =
                    pedido.Cep;

                worksheet.Cell(linha, 9).Value =
                    pedido.Cidade;

                worksheet.Cell(linha, 10).Value =
                    pedido.Estado;

                worksheet.Cell(linha, 11).Value =
                    pedido.TotalItensPedido;

                worksheet.Cell(linha, 12).Value =
                    pedido.PedidoTotal;

                linha++;
            }


            // =========================================================
            // FORMATAÇÃO DO CABEÇALHO
            // =========================================================

            var cabecalho =
                worksheet.Range(
                    linhaCabecalho,
                    1,
                    linhaCabecalho,
                    12);

            cabecalho.Style.Font.Bold = true;

            cabecalho.Style.Alignment.Horizontal =
                XLAlignmentHorizontalValues.Center;

            cabecalho.Style.Alignment.Vertical =
                XLAlignmentVerticalValues.Center;

            worksheet.Row(linhaCabecalho).Height = 25;


            // =========================================================
            // FORMATAÇÃO DAS DATAS
            // =========================================================

            worksheet.Column(2).Style.DateFormat.Format =
                "dd/MM/yyyy HH:mm";


            // =========================================================
            // FORMATAÇÃO DOS VALORES
            // =========================================================

            worksheet.Column(11).Style.NumberFormat.Format =
                "0";

            worksheet.Column(12).Style.NumberFormat.Format =
                "\"R$\" #,##0.00";


            // =========================================================
            // TABELA / FILTRO
            // =========================================================

            if (pedidos.Count > 0)
            {
                var tabela = worksheet.Range(
                    linhaCabecalho,
                    1,
                    linha - 1,
                    12);

                tabela.Style.Border.OutsideBorder =
                    XLBorderStyleValues.Thin;

                tabela.Style.Border.InsideBorder =
                    XLBorderStyleValues.Thin;

                tabela.SetAutoFilter();
            }


            // =========================================================
            // RESUMO FINAL
            // =========================================================

            var linhaResumo = linha + 1;

            worksheet.Cell(linhaResumo, 10).Value =
                "Total de Pedidos";

            worksheet.Cell(linhaResumo, 11).Value =
                pedidos.Count;

            worksheet.Cell(linhaResumo + 1, 10).Value =
                "Total de Itens";

            worksheet.Cell(linhaResumo + 1, 11).Value =
                pedidos.Sum(p => p.TotalItensPedido);

            worksheet.Cell(linhaResumo + 2, 10).Value =
                "Faturamento Total";

            worksheet.Cell(linhaResumo + 2, 11).Value =
                pedidos.Sum(p => p.PedidoTotal);


            // Formatação do resumo

            var resumo = worksheet.Range(
                linhaResumo,
                10,
                linhaResumo + 2,
                11);

            resumo.Style.Font.Bold = true;

            resumo.Style.Border.OutsideBorder =
                XLBorderStyleValues.Thin;

            resumo.Style.Border.InsideBorder =
                XLBorderStyleValues.Thin;


            worksheet.Cell(linhaResumo, 11)
                .Style.NumberFormat.Format = "0";

            worksheet.Cell(linhaResumo + 1, 11)
                .Style.NumberFormat.Format = "0";

            worksheet.Cell(linhaResumo + 2, 11)
                .Style.NumberFormat.Format =
                    "\"R$\" #,##0.00";


            // =========================================================
            // DATA DA EXPORTAÇÃO
            // =========================================================

            worksheet.Cell(linhaResumo + 4, 10).Value =
                "Exportado em:";

            worksheet.Cell(linhaResumo + 4, 11).Value =
                DateTime.Now;

            worksheet.Cell(linhaResumo + 4, 11)
                .Style.DateFormat.Format =
                    "dd/MM/yyyy HH:mm";


            // =========================================================
            // CONGELAR CABEÇALHO
            // =========================================================

            worksheet.SheetView.FreezeRows(
                linhaCabecalho);


            // =========================================================
            // AJUSTAR COLUNAS
            // =========================================================

            worksheet.Columns().AdjustToContents();

            for (int coluna = 1; coluna <= 12; coluna++)
            {
                if (worksheet.Column(coluna).Width > 35)
                {
                    worksheet.Column(coluna).Width = 35;
                }
            }


            // =========================================================
            // GERAR ARQUIVO
            // =========================================================

            using var stream = new MemoryStream();

            workbook.SaveAs(stream);

            stream.Position = 0;

            var nomeArquivo =
                $"Pedidos_{DateTime.Now:yyyy-MM-dd_HH-mm}.xlsx";

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                nomeArquivo);
        }
        //Remover daqui para cima
    }
}