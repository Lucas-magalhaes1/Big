using System.IO;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Big.Models;

namespace Big.Services
{
    public class PedidoPdfService
    {
        public async Task<byte[]> GerarPdfPedidoAsync(Pedido pedido)
        {
            return await Task.Run(() =>
            {
                using var stream = new MemoryStream();

                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(30);
                        
                        page.Header().Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text("Minha Empresa LTDA").Bold().FontSize(16);
                                col.Item().Text("CNPJ: 12.345.678/0001-99");
                                col.Item().Text("Rua Exemplo, 123 - Cidade - Estado");
                                col.Item().Text("Telefone: (11) 99999-9999");
                            });

                            row.ConstantItem(100).Image("wwwroot/logo.png");
                        });
                        
                        page.Content().Column(col =>
                        {
                            col.Spacing(10);
                            col.Item().Text($"Pedido #{pedido.Id}").SemiBold().FontSize(18);
                            col.Item().Text($"Data do Pedido: {pedido.DataPedido:dd/MM/yyyy HH:mm}");
                            col.Item().Text($"Status: {pedido.Status}");

                            col.Item().LineHorizontal(1);

                            
                            col.Item().Text($"Cliente: {pedido.Cliente.Nome}");
                            col.Item().Text($"CPF: {pedido.Cliente.Documento}");
                            col.Item().Text($"Endereço: {pedido.Cliente.Endereco}");

                            col.Item().LineHorizontal(1);

                            
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(50);
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(80);
                                    columns.ConstantColumn(80);
                                    columns.ConstantColumn(100);
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Text("ID");
                                    header.Cell().Text("Produto");
                                    header.Cell().Text("Qtd");
                                    header.Cell().Text("Unitário");
                                    header.Cell().Text("Subtotal");
                                });

                                foreach (var item in pedido.Produtos)
                                {
                                    decimal subtotal = item.Quantidade * item.PrecoUnitario;

                                    table.Cell().Text(item.Produto.Id.ToString());
                                    table.Cell().Text(item.Produto.Nome);
                                    table.Cell().Text(item.Quantidade.ToString());
                                    table.Cell().Text($"R$ {item.PrecoUnitario:F2}");
                                    table.Cell().Text($"R$ {subtotal:F2}"); 
                                }

                            });

                            col.Item().LineHorizontal(1);
                            col.Item().AlignRight().Text($"Total: R$ {pedido.Total:F2}").Bold();
                            col.Item().AlignRight().Text($"Forma de Pagamento: {pedido.FormaPagamento}");
                        });
                        page.Footer()
                            .AlignCenter()
                            .Text("Obrigado por comprar conosco!");
                    });
                }).GeneratePdf(stream);

                return stream.ToArray();
            });
        }
    }
}
