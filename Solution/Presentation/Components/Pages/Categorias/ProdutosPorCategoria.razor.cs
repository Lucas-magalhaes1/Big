using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Produtos
{
    public class ProdutosPorCategoriaBase : ComponentBase
    {
        [Parameter] public int categoriaId { get; set; }

        [Inject] protected ProdutoService ProdutoService { get; set; }
        [Inject] protected CategoriaService CategoriaService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected List<Produto>? produtos;
        protected string? categoriaNome;
        protected FiltroProduto filtros = new();

        protected override async Task OnInitializedAsync()
        {
            categoriaNome = (await CategoriaService.ObterPorIdAsync(categoriaId))?.Nome ?? "Desconhecida";
            produtos = await ProdutoService.BuscarPorCategoriaAsync(categoriaId);
        }

        protected async Task AplicarFiltrosAsync()
        {
            try
            {
                if (filtros.PrecoMin < 0 || filtros.PrecoMax < 0)
                {
                    Console.WriteLine("Os preços não podem ser negativos.");
                    return;
                }

                produtos = await ProdutoService.BuscarComFiltrosAsync(filtros.Nome, filtros.PrecoMin, filtros.PrecoMax);
                produtos = produtos.Where(p => p.CategoriaId == categoriaId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao aplicar filtros: {ex.Message}");
            }
        }

        protected async Task RemoverFiltros()
        {
            filtros = new FiltroProduto();
            produtos = await ProdutoService.BuscarPorCategoriaAsync(categoriaId);
        }

        protected void VoltarParaCategorias()
        {
            Navigation.NavigateTo("/Categorias");
        }

        public class FiltroProduto
        {
            public string? Nome { get; set; }
            public decimal? PrecoMin { get; set; }
            public decimal? PrecoMax { get; set; }
        }
    }
}
