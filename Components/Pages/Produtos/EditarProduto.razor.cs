using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Produtos
{
    public class EditarProdutoBase : ComponentBase
    {
        [Parameter] public int Id { get; set; }

        [Inject] protected ProdutoService ProdutoService { get; set; }
        [Inject] protected CategoriaService CategoriaService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected Produto produto = new Produto();
        protected List<Categoria> categorias = new();
        protected bool mostrarFeedback = false;

        protected override async Task OnInitializedAsync()
        {
            categorias = await CategoriaService.ObterTodasAsync();
            produto = await ProdutoService.ObterPorIdAsync(Id);

            if (produto == null)
            {
                Navigation.NavigateTo("/Produtos");
            }
        }

        protected async Task OnValidSubmitAsync()
        {
            try
            {
                if (produto.CategoriaId == null || produto.CategoriaId == 0)
                {
                    Console.WriteLine("A categoria do produto é obrigatória.");
                    return;
                }

                await ProdutoService.AtualizarAsync(produto);
                mostrarFeedback = true;

                await Task.Delay(2000);
                Navigation.NavigateTo("/Produtos");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar o produto: {ex.Message}");
            }
        }
    }
}