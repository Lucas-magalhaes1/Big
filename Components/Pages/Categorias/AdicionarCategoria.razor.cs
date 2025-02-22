using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Big.Models;
using Big.Services;

namespace Big.Pages.Categorias
{
    public class AdicionarCategoriaBase : ComponentBase
    {
        [Inject] protected CategoriaService CategoriaService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected Categoria novaCategoria = new();
        protected bool mostrarFeedback = false;

        protected async Task OnValidSubmitAsync()
        {
            try
            {
                await CategoriaService.AdicionarAsync(novaCategoria);
                mostrarFeedback = true;
                StateHasChanged();

                await Task.Delay(2000);
                Navigation.NavigateTo("/Categorias");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar a categoria: {ex.Message}");
            }
        }
    }
}