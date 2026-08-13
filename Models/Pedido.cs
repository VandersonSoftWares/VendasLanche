using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCLanche.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Nome")]
        [StringLength(50,
            ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Sobrenome")]
        [StringLength(50,
            ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres.")]
        public string Sobrenome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Endereço")]
        [StringLength(100,
            ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres.")]
        public string Endereco1 { get; set; } = string.Empty;

        [Display(Name = "Complemento")]
        [StringLength(100,
            ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres.")]
        public string? Endereco2 { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "CEP")]
        [StringLength(10, MinimumLength = 8,
            ErrorMessage = "O campo {0} deve possuir entre 8 e 10 caracteres.")]
        public string Cep { get; set; } = string.Empty;

        [Display(Name = "Estado")]
        [StringLength(50,
            ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres.")]
        public string? Estado { get; set; } = string.Empty;

        [Display(Name = "Cidade")]
        [StringLength(50,
            ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres.")]
        public string? Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Telefone")]
        [StringLength(25,
            ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres.")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "E-mail")]
        [StringLength(50,
            ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        [ScaffoldColumn(false)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Total do Pedido")]
        public decimal PedidoTotal { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Itens no Pedido")]
        public int TotalItensPedido { get; set; }

        [Display(Name = "Data do Pedido")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime PedidoEnviado { get; set; }

        [Display(Name = "Data de Entrega")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? PedidoEntregueEm { get; set; }

        [ValidateNever]
        public List<PedidoDetalhe> PedidoItens { get; set; } = new();
    }
}