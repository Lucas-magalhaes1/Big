using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.GestaoPedidos
{
    public class PedidoEditarBase : ComponentBase
    {
        [Parameter] public int id { get; set; }

        [Inject] protected PedidoService PedidoService { get; set; }
        [Inject] protected ClienteService ClienteService { get; set; }
        [Inject] protected ProdutoService ProdutoService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected Pedido? pedido;
        protected List<Cliente>? clientes = new();
        protected List<Produto>? produtos = new();
        protected List<ProdutoPedido> produtosPedido = new();

        protected int? produtoSelecionadoId;
        protected int quantidadeSelecionada = 1;

        protected override async Task OnInitializedAsync()
        {
            pedido = await PedidoService.ObterPorIdAsync(id);
            clientes = await ClienteService.ObterTodosAsync() ?? new List<Cliente>();
            produtos = await ProdutoService.ObterTodosAsync() ?? new List<Produto>();

            if (pedido != null)
            {
                produtosPedido = pedido.Produtos.ToList();
            }
        }

        protected void AdicionarProduto()
        {
            if (produtoSelecionadoId.HasValue && quantidadeSelecionada > 0)
            {
                var produto = produtos.FirstOrDefault(p => p.Id == produtoSelecionadoId);
                if (produto != null)
                {
                    var itemPedido = new ProdutoPedido
                    {
                        ProdutoId = produto.Id,
                        Produto = produto,
                        Quantidade = quantidadeSelecionada,
                        PrecoUnitario = produto.Preco
                    };
                    produtosPedido.Add(itemPedido);
                    AtualizarTotal();
                }
            }
        }

        protected void RemoverProduto(ProdutoPedido item)
        {
            produtosPedido.Remove(item);
            AtualizarTotal();
        }

        protected void AtualizarTotal()
        {
            if (pedido != null)
            {
                pedido.Total = produtosPedido.Sum(p => p.Quantidade * p.PrecoUnitario);
            }
        }

        protected async Task AtualizarPedidoAsync()
        {
            if (pedido != null)
            {
                await PedidoService.AtualizarAsync(pedido);
                Navigation.NavigateTo("/Pedidos");
            }
        }
    }
}
