using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Big.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; } 

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do produto deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }  = string.Empty;

        [Required(ErrorMessage = "A descrição do produto é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição do produto deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço do produto é obrigatório.")]
        [Range(0.01, 1000000, ErrorMessage = "O preço deve ser maior que zero e menor que 1 milhão.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A quantidade em estoque da loja é obrigatória.")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser positiva.")]
        public int EstoqueLoja { get; set; }

        [Required(ErrorMessage = "A quantidade em estoque do centro de distribuição é obrigatória.")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser positiva.")]
        public int EstoqueCentroDistribuicao { get; set; }

        [Url(ErrorMessage = "A URL da imagem deve ser válida.")]
        [StringLength(300, ErrorMessage = "A URL da imagem deve ter no máximo 300 caracteres.")]
        
        public string ImagemUrl { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; } 
        
        
        public Produto() { }
        
        public Produto(string nome, string descricao, decimal preco, int estoqueLoja, int estoqueCentroDistribuicao, string imagemUrl, int categoriaId)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            EstoqueLoja = estoqueLoja;
            EstoqueCentroDistribuicao = estoqueCentroDistribuicao;
            ImagemUrl = imagemUrl;
            CategoriaId = categoriaId;
        }
    }
}