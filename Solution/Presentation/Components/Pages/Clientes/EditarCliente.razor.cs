using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Clientes
{
    public class EditarClienteBase : ComponentBase
    {
        [Parameter] public int id { get; set; }

        [Inject] protected ClienteService ClienteService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected Cliente? cliente;
        protected bool mostrarFeedback = false;

        protected override async Task OnInitializedAsync()
        {
            cliente = await ClienteService.ObterPorIdAsync(id);
        }

        protected async Task OnValidSubmitAsync()
        {
            try
            {
                await ClienteService.AtualizarAsync(cliente);
                mostrarFeedback = true;
                StateHasChanged();

                await Task.Delay(2000);
                Navigation.NavigateTo("/Clientes");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao editar o cliente: {ex.Message}");
            }
        }
    }
}