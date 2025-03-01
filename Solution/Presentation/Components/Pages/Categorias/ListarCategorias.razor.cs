using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Categorias
{
    public class ListarCategoriasBase : ComponentBase
    {
        [Inject] protected CategoriaService CategoriaService { get; set; }

        protected List<Categoria>? categorias;

        protected override async Task OnInitializedAsync()
        {
            categorias = await CategoriaService.ObterTodasAsync();
        }
    }
}