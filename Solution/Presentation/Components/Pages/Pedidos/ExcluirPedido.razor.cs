using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Pedidos
{
    public class ExcluirPedidoBase : ComponentBase
    {
        [Parameter] public int id { get; set; }

        [Inject] protected PedidoService PedidoService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected Pedido? pedido;

        protected override async Task OnInitializedAsync()
        {
            pedido = await PedidoService.ObterPorIdAsync(id);
        }

        protected async Task ExcluirPedidoAsync()
        {
            if (pedido != null)
            {
                await PedidoService.ExcluirAsync(id);
                Navigation.NavigateTo("/Pedidos");
            }
        }
    }
}