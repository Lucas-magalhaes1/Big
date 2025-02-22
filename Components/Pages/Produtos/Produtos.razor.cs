using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;
using System.Globalization;

namespace Big.Pages.Produtos
{
    public class ProdutosBase : ComponentBase
    {
        [Inject] protected ProdutoService ProdutoService { get; set; }
        [Inject] protected CategoriaService CategoriaService { get; set; }

        protected List<Produto>? produtos;
        protected List<Categoria>? categorias;
        protected FiltroProduto filtros = new();

        protected override async Task OnInitializedAsync()
        {
            categorias = await CategoriaService.ObterTodasAsync();
            produtos = await ProdutoService.ObterTodosAsync();
        }

        protected async Task OnValidSubmitAsync()
        {
            try
            {
                if (filtros.PrecoMin < 0 || filtros.PrecoMax < 0)
                {
                    Console.WriteLine("Os preços não podem ser negativos.");
                    return;
                }

                produtos = filtros.CategoriaId.HasValue
                    ? await ProdutoService.BuscarPorCategoriaAsync(filtros.CategoriaId.Value)
                    : await ProdutoService.BuscarComFiltrosAsync(filtros.Nome, filtros.PrecoMin, filtros.PrecoMax);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao aplicar filtros: {ex.Message}");
            }
        }

        protected async Task RemoverFiltros()
        {
            filtros = new FiltroProduto();
            produtos = await ProdutoService.ObterTodosAsync();
        }

        public class FiltroProduto
        {
            public string? Nome { get; set; }
            public decimal? PrecoMin { get; set; }
            public decimal? PrecoMax { get; set; }
            public int? CategoriaId { get; set; }
        }
    }
}