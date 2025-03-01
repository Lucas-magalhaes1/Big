using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Produtos
{
    public class AdicionarProdutoBase : ComponentBase
    {
        [Inject] protected ProdutoService ProdutoService { get; set; }
        [Inject] protected CategoriaService CategoriaService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected Produto novoProduto = new Produto();
        protected List<Categoria> categorias = new();

        protected override async Task OnInitializedAsync()
        {
            categorias = await CategoriaService.ObterTodasAsync();
        }

        protected async Task OnValidSubmitAsync()
        {
            try
            {
                if (novoProduto.CategoriaId == null || novoProduto.CategoriaId == 0)
                {
                    Console.WriteLine("A categoria do produto é obrigatória.");
                    return;
                }

                await ProdutoService.AdicionarAsync(novoProduto);
                Navigation.NavigateTo("/Produtos");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar o produto: {ex.Message}");
            }
        }
    }
}