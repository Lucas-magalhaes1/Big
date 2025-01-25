using System.ComponentModel.DataAnnotations;

namespace Big.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da categoria deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }
        
        [StringLength(300, ErrorMessage = "A descrição deve ter no máximo 300 caracteres.")]
        public string? Descricao { get; set; }

        [Url(ErrorMessage = "A URL da imagem deve ser válida.")]
        [StringLength(300, ErrorMessage = "A URL da imagem deve ter no máximo 300 caracteres.")]
        public string ImagemUrl { get; set; }
    }
}
