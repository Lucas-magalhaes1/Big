using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Big.Models;

public class ProdutoPedido
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int PedidoId { get; set; }

    [ForeignKey("PedidoId")]
    public Pedido Pedido { get; set; }

    [Required]
    public int ProdutoId { get; set; }

    [ForeignKey("ProdutoId")]
    public Produto Produto { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal PrecoUnitario { get; set; }
}
