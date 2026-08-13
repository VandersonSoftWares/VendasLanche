using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCLanche.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "O nome da Categoria deve ser informado")]
        [Display(Name = "Nome da Categoria")]
        [StringLength(60, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres.")]
        public string CategoriaNome { get; set; }
        [Display(Name = "Descrição da Categoria")]
        [StringLength(200, ErrorMessage = "O campo {0} deve possuir no máximo {1} caracteres.")]
        public string Descricao { get; set; }
        public List<Lanche> Lanches { get; set; } = new List<Lanche>();
    }
}
