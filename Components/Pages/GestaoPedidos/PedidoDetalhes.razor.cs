using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.GestaoPedidos
{
    public class PedidoDetalhesBase : ComponentBase
    {
        [Parameter] public int id { get; set; }

        [Inject] protected PedidoService PedidoService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected Pedido? pedido;
        protected string vendedorMock;

        protected override async Task OnInitializedAsync()
        {
            pedido = await PedidoService.ObterPorIdAsync(id);

            if (pedido != null)
            {
                // Simula vendedores aleatórios
                var vendedoresMock = new List<string> { "João Silva", "Maria Oliveira", "Carlos Souza", "Ana Pereira" };
                var random = new Random();
                vendedorMock = vendedoresMock[random.Next(vendedoresMock.Count)];
            }
        }
    }
}