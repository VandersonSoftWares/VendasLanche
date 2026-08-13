using System.ComponentModel.DataAnnotations;

namespace MVCLanche.Models
{
    public class CarrinhoCompraItem
    {
        [Key]
        public int CarrinhoCompraItemId { get; set; }

        public int LancheId { get; set; }

        public virtual Lanche Lanche { get; set; } = null!;

        [Display(Name = "Quantidade")]
        [Range(1, 100,
            ErrorMessage = "O campo {0} deve estar entre {1} e {2}.")]
        public int Quantidade { get; set; }

        public int CarrinhoCompraId { get; set; }
    }
}