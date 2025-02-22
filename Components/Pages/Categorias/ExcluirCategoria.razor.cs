using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Categorias
{
    public class ExcluirCategoriaBase : ComponentBase
    {
        [Parameter] public int Id { get; set; }

        [Inject] protected CategoriaService CategoriaService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected Categoria? categoria;

        protected override async Task OnInitializedAsync()
        {
            categoria = await CategoriaService.ObterPorIdAsync(Id);

            if (categoria == null)
            {
                Navigation.NavigateTo("/Categorias");
            }
        }

        protected async Task ConfirmarExclusao()
        {
            try
            {
                await CategoriaService.ExcluirAsync(Id);
                Navigation.NavigateTo("/Categorias");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir a categoria: {ex.Message}");
            }
        }
    }
}