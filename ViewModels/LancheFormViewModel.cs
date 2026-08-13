using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MVCLanche.ViewModels
{
    public class LancheFormViewModel
    {
        public int LancheId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Nome do Lanche")]
        [StringLength(60)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Descrição Curta")]
        public string DescricaoCurta { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Descrição Detalhada")]
        public string DescricaoDetalhada { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Preço")]
        [Range(1, 999.99)]
        public decimal Preco { get; set; }

        [Display(Name = "Imagem")]
        public IFormFile? ImagemArquivo { get; set; }

        public string? ImagemUrl { get; set; }

        public string? ImagemThumbnailUrl { get; set; }

        [Display(Name = "Lanche Preferido")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Em Estoque")]
        public bool EmEstoque { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria.")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
    }
}