using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Produtos
{
    public class EstoqueBase : ComponentBase
    {
        [Inject] protected ProdutoService ProdutoService { get; set; }

        protected List<Produto> produtos = new();
        protected string? mensagemSucesso;
        protected string? filtroNome;
        protected int? filtroQuantidadeMinima;
        protected int? filtroQuantidadeMaxima;
        protected string tipoEstoque = "Loja"; 

        protected override async Task OnInitializedAsync()
        {
            produtos = await ProdutoService.ObterTodosAsync();
        }

        protected async Task OnValidSubmitAsync()
        {
            try
            {
                foreach (var produto in produtos)
                {
                    await ProdutoService.AtualizarAsync(produto);
                }

                mensagemSucesso = "Estoque atualizado com sucesso!";
                StateHasChanged();

                await Task.Delay(3000);
                mensagemSucesso = null;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar o estoque: {ex.Message}");
            }
        }

        protected async Task FiltrarProdutos()
        {
            produtos = await ProdutoService.ObterTodosAsync();

            if (!string.IsNullOrWhiteSpace(filtroNome))
            {
                produtos = produtos.Where(p => p.Nome.Contains(filtroNome, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (filtroQuantidadeMinima.HasValue || filtroQuantidadeMaxima.HasValue)
            {
                produtos = tipoEstoque switch
                {
                    "Loja" => produtos.Where(p =>
                        (!filtroQuantidadeMinima.HasValue || p.EstoqueLoja >= filtroQuantidadeMinima) &&
                        (!filtroQuantidadeMaxima.HasValue || p.EstoqueLoja <= filtroQuantidadeMaxima)).ToList(),

                    "Centro" => produtos.Where(p =>
                        (!filtroQuantidadeMinima.HasValue || p.EstoqueCentroDistribuicao >= filtroQuantidadeMinima) &&
                        (!filtroQuantidadeMaxima.HasValue || p.EstoqueCentroDistribuicao <= filtroQuantidadeMaxima)).ToList(),

                    _ => produtos
                };
            }
        }

        protected void RemoverFiltros()
        {
            filtroNome = null;
            filtroQuantidadeMinima = null;
            filtroQuantidadeMaxima = null;
            tipoEstoque = "Loja"; 
            StateHasChanged();

            _ = OnInitializedAsync();
        }

        protected void ReabastecerEstoque(Produto produto, int quantidade)
        {
            produto.EstoqueLoja += quantidade;
            produto.EstoqueCentroDistribuicao += quantidade;
        }
    }
}
