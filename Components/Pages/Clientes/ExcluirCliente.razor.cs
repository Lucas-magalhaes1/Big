using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Clientes
{
    public class ExcluirClienteBase : ComponentBase
    {
        [Parameter] public int id { get; set; }

        [Inject] protected ClienteService ClienteService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected Cliente? cliente;

        protected override async Task OnInitializedAsync()
        {
            cliente = await ClienteService.ObterPorIdAsync(id);
        }

        protected async Task ExcluirClienteAsync()
        {
            try
            {
                await ClienteService.ExcluirAsync(id);
                Navigation.NavigateTo("/Clientes");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir o cliente: {ex.Message}");
            }
        }
    }
}