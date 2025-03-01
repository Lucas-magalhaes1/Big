using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Clientes
{
    public class AdicionarClienteBase : ComponentBase
    {
        [Inject] protected ClienteService ClienteService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected Cliente novoCliente = new Cliente();
        protected bool mostrarFeedback = false;

        protected async Task OnValidSubmitAsync()
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(novoCliente.Nome) ||
                    string.IsNullOrWhiteSpace(novoCliente.Email) ||
                    string.IsNullOrWhiteSpace(novoCliente.Telefone) ||
                    string.IsNullOrWhiteSpace(novoCliente.Documento) ||
                    string.IsNullOrWhiteSpace(novoCliente.Endereco))
                {
                    Console.WriteLine("Todos os campos obrigatórios devem ser preenchidos.");
                    return;
                }
                
                if (novoCliente.DataCadastro == default)
                {
                    novoCliente.DataCadastro = DateTime.Now;
                }

                await ClienteService.AdicionarAsync(novoCliente);
                mostrarFeedback = true;
                StateHasChanged();

                await Task.Delay(2000);
                Navigation.NavigateTo("/Clientes");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar o cliente: {ex.Message}");
            }
        }
    }
}