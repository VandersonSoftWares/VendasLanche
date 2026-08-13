using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVCLanche.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Nome do Lanche")]
        [StringLength(60,
            ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Descrição Curta")]
        [MinLength(20,
            ErrorMessage = "O campo {0} deve possuir no mínimo 20 caracteres.")]
        [StringLength(200,
            ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres.")]
        public string DescricaoCurta { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Descrição Detalhada")]
        [MinLength(20,
            ErrorMessage = "O campo {0} deve possuir no mínimo 20 caracteres.")]
        [StringLength(200,
            ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres.")]
        public string DescricaoDetalhada { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 999.99,
            ErrorMessage = "O campo {0} deve estar entre R$ 1,00 e R$ 999,99.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Caminho da Imagem")]
        [StringLength(200,
            ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres.")]
        public string ImagemUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Miniatura da Imagem")]
        [StringLength(200,
            ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres.")]
        public string ImagemThumbnailUrl { get; set; } = string.Empty;

        [Display(Name = "Lanche Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Em Estoque")]
        public bool EmEstoque { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [ValidateNever]
        public virtual Categoria Categoria { get; set; } = null!;
    }
}