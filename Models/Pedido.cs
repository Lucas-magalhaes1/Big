using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Big.Models;

public class Pedido
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ClienteId { get; set; }

    [ForeignKey("ClienteId")]
    public Cliente Cliente { get; set; }

    [Required]
    public DateTime DataPedido { get; set; } = DateTime.Now;

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Aguardando";

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    public List<ProdutoPedido> Produtos { get; set; } = new();
}