using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Produtos
{
    public class ExcluirProdutoBase : ComponentBase
    {
        [Parameter] public int Id { get; set; }

        [Inject] protected ProdutoService ProdutoService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected Produto produto = new();
        protected bool mostrarFeedback = false;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                produto = await ProdutoService.ObterPorIdAsync(Id);
                if (produto == null)
                {
                    Navigation.NavigateTo("/Produtos");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar o produto: {ex.Message}");
                Navigation.NavigateTo("/Produtos");
            }
        }

        protected async Task ExcluirProduto()
        {
            try
            {
                await ProdutoService.ExcluirAsync(Id);
                
                mostrarFeedback = true;
                StateHasChanged();
                
                await Task.Delay(1000);
                Navigation.NavigateTo("/Produtos");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir o produto: {ex.Message}");
            }
        }
    }
}