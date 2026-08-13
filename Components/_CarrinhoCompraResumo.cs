using Microsoft.AspNetCore.Mvc;
using MVCLanche.Models;
using MVCLanche.ViewModels;
using System.Threading.Tasks;

namespace MVCLanche.Components
{
    public class CarrinhoCompraResumoViewComponent : ViewComponent
    {
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraResumoViewComponent(CarrinhoCompra carrinhoCompra)
        {
            _carrinhoCompra = carrinhoCompra;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var itens = await Task.Run(() =>
                _carrinhoCompra.GetCarrinhoCompraItens());

            _carrinhoCompra.CarrinhoCompraItems = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhoCompraVM);
        }
    }
}