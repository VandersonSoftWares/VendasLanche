using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCLanche.Models
{
    public class PedidoDetalhe
    {
        public int PedidoDetalheId { get; set; }

        public int PedidoId { get; set; }

        public int LancheId { get; set; }

        [Display(Name = "Quantidade")]
        [Range(1, 100,
            ErrorMessage = "O campo {0} deve estar entre {1} e {2}.")]
        public int Quantidade { get; set; }

        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999.99,
            ErrorMessage = "O campo {0} deve estar entre R$ 0,01 e R$ 999,99.")]
        public decimal Preco { get; set; }

        public virtual Lanche Lanche { get; set; } = null!;

        public virtual Pedido Pedido { get; set; } = null!;
    }
}