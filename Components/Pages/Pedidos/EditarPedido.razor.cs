using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Pedidos
{
    public class EditarPedidoBase : ComponentBase
    {
        [Parameter] public int id { get; set; }

        [Inject] protected PedidoService PedidoService { get; set; }
        [Inject] protected ProdutoService ProdutoService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected Pedido? pedido;
        protected List<Produto>? produtosDisponiveis;
        protected int? produtoSelecionadoId;
        protected int quantidadeSelecionada = 1;

        protected override async Task OnInitializedAsync()
        {
            pedido = await PedidoService.ObterPorIdAsync(id);
            produtosDisponiveis = await ProdutoService.ObterTodosAsync() ?? new List<Produto>();

            if (pedido == null)
            {
                pedido = new Pedido { Produtos = new List<ProdutoPedido>() };
            }
        }

        protected void AdicionarProduto()
        {
            if (produtoSelecionadoId.HasValue && quantidadeSelecionada > 0)
            {
                var produto = produtosDisponiveis.FirstOrDefault(p => p.Id == produtoSelecionadoId);
                if (produto != null)
                {
                    var itemPedido = new ProdutoPedido
                    {
                        PedidoId = pedido.Id,
                        ProdutoId = produto.Id,
                        Produto = produto,
                        Quantidade = quantidadeSelecionada,
                        PrecoUnitario = produto.Preco
                    };

                    pedido.Produtos.Add(itemPedido);
                    AtualizarTotal();
                }
            }
        }
        
        protected void AtualizarQuantidade(ProdutoPedido item, int novaQuantidade)
        {
            if (novaQuantidade > 0)
            {
                item.Quantidade = novaQuantidade;
                AtualizarTotal();
            }
        }
        
        protected void RemoverProduto(ProdutoPedido item)
        {
            pedido.Produtos.Remove(item);
            AtualizarTotal();
        }

        protected void AtualizarTotal()
        {
            pedido.Total = pedido.Produtos.Sum(p => p.Quantidade * p.PrecoUnitario);
        }

        protected async Task AtualizarPedidoAsync()
        {
            if (pedido != null)
            {
                AtualizarTotal(); 
                await PedidoService.AtualizarAsync(pedido);
                Navigation.NavigateTo("/Pedidos");
            }
        }
    }
}
