using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Big.Pages.Pedidos
{
    public class DetalhesPedidoBase : ComponentBase
    {
        [Parameter] public int id { get; set; }

        [Inject] protected PedidoService PedidoService { get; set; } = default!;
        [Inject] protected PedidoPdfService PedidoPdfService { get; set; } = default!;
        [Inject] protected IJSRuntime JS { get; set; } = default!;
        
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        protected Pedido? pedido;

        protected override async Task OnInitializedAsync()
        {
            pedido = await PedidoService.ObterPorIdAsync(id);
        }

        protected async Task BaixarPdf()
        {
            if (pedido == null || pedido.Status != "Aprovado") return;

            try
            {
                var pdfBytes = await PedidoPdfService.GerarPdfPedidoAsync(pedido);
                var base64Pdf = Convert.ToBase64String(pdfBytes);

                await JS.InvokeVoidAsync("downloadPdf", base64Pdf, $"Pedido_{pedido.Id}.pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar PDF: {ex.Message}");
            }
        }
        protected async Task DuplicarPedido(int pedidoId)
        {
            var pedidoOriginal = await PedidoService.ObterPorIdAsync(pedidoId);
            if (pedidoOriginal == null) return;

            var novoPedido = new Pedido
            {
                ClienteId = pedidoOriginal.ClienteId,
                DataPedido = DateTime.Now, 
                Status = "Aguardando", 
                FormaPagamento = pedidoOriginal.FormaPagamento,
                Total = pedidoOriginal.Produtos.Sum(p => p.Quantidade * p.PrecoUnitario), 
                Produtos = pedidoOriginal.Produtos.Select(p => new ProdutoPedido
                {
                    ProdutoId = p.ProdutoId,
                    Quantidade = p.Quantidade,
                    PrecoUnitario = p.PrecoUnitario
                }).ToList()
            };

            var novoPedidoId = await PedidoService.AdicionarERetornarIdAsync(novoPedido);
            Navigation.NavigateTo($"/Pedidos/Editar/{novoPedidoId}"); 
        }

    }
}