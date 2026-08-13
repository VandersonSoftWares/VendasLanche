using MVCLanche.Models;

namespace MVCLanche.ViewModels
{
    public class CarrinhoCompraViewModel
    {
        public required CarrinhoCompra CarrinhoCompra { get; set; }

        public decimal CarrinhoCompraTotal { get; set; }
    }
}