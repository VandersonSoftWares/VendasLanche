using Microsoft.EntityFrameworkCore;
using MVCLanche.Context;

namespace MVCLanche.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public int CarrinhoCompraId { get; set; }

        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; } = new();

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            var httpContext = services.GetRequiredService<IHttpContextAccessor>().HttpContext
                ?? throw new InvalidOperationException("O contexto HTTP não está disponível.");

            ISession session = httpContext.Session;

            var context = services.GetRequiredService<AppDbContext>();

            string? carrinhoIdSessao = session.GetString("CarrinhoId");

            if (string.IsNullOrEmpty(carrinhoIdSessao))
            {
                carrinhoIdSessao = (DateTime.Now.Ticks % 1_000_000_000).ToString();
                session.SetString("CarrinhoId", carrinhoIdSessao);
            }

            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = int.Parse(carrinhoIdSessao)
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens
                .SingleOrDefault(s =>
                    s.LancheId == lanche.LancheId &&
                    s.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    LancheId = lanche.LancheId,
                    Quantidade = 1
                };

                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }

            _context.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens
                .SingleOrDefault(s =>
                    s.LancheId == lanche.LancheId &&
                    s.CarrinhoCompraId == CarrinhoCompraId);

            int quantidadeLocal = 0;

            if (carrinhoCompraItem != null)
            {
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }

                _context.SaveChanges();
            }

            return quantidadeLocal;
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItems.Count != 0
                ? CarrinhoCompraItems
                : CarrinhoCompraItems = _context.CarrinhoCompraItens
                    .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                    .Include(c => c.Lanche)
                    .ToList();
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId);

            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);

            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            return _context.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Lanche.Preco * c.Quantidade)
                .Sum();
        }
    }
}