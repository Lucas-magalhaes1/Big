using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Categorias
{
    public class EditarCategoriaBase : ComponentBase
    {
        [Parameter] public int Id { get; set; }

        [Inject] protected CategoriaService CategoriaService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected Categoria? categoria;
        protected bool mostrarFeedback = false;

        protected override async Task OnInitializedAsync()
        {
            categoria = await CategoriaService.ObterPorIdAsync(Id);

            if (categoria == null)
            {
                Navigation.NavigateTo("/Categorias");
            }
        }

        protected async Task OnValidSubmitAsync()
        {
            try
            {
                await CategoriaService.AtualizarAsync(categoria);
                mostrarFeedback = true;
                StateHasChanged();

                await Task.Delay(2000);
                Navigation.NavigateTo("/Categorias");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar a categoria: {ex.Message}");
            }
        }
    }
}