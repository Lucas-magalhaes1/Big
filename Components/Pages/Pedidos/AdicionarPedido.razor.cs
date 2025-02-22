using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Pedidos
{
    public class AdicionarPedidoBase : ComponentBase
    {
        [Inject] protected PedidoService PedidoService { get; set; }
        [Inject] protected ClienteService ClienteService { get; set; }
        [Inject] protected ProdutoService ProdutoService { get; set; }
        [Inject] protected CategoriaService CategoriaService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected Pedido novoPedido = new();
        protected List<Cliente>? clientes = new();
        protected List<Cliente> clientesFiltrados = new();
        protected string filtroCliente = "";

        protected List<Produto>? produtos = new();
        protected List<Produto> produtosFiltrados = new();
        protected List<ProdutoPedido> produtosPedido = new();
        protected List<Categoria>? categorias = new();
        protected string filtroProduto = "";
        protected int? filtroCategoria;

        protected override async Task OnInitializedAsync()
        {
            clientes = await ClienteService.ObterTodosAsync() ?? new List<Cliente>();
            produtos = await ProdutoService.ObterTodosAsync() ?? new List<Produto>();
            categorias = await CategoriaService.ObterTodasAsync();
            clientesFiltrados = clientes.ToList();
            produtosFiltrados = produtos.ToList();
        }

        protected void FiltrarClientes()
        {
            clientesFiltrados = string.IsNullOrWhiteSpace(filtroCliente)
                ? clientes.ToList()
                : clientes.Where(c => c.Nome.Contains(filtroCliente, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        protected void FiltrarProdutos()
        {
            produtosFiltrados = produtos
                .Where(p =>
                    (string.IsNullOrWhiteSpace(filtroProduto) || p.Nome.Contains(filtroProduto, StringComparison.OrdinalIgnoreCase)) &&
                    (!filtroCategoria.HasValue || p.CategoriaId == filtroCategoria))
                .ToList();
        }

        protected void AdicionarProduto(Produto produto)
        {
            if (produto.EstoqueCentroDistribuicao <= 0) return;

            var existente = produtosPedido.FirstOrDefault(p => p.ProdutoId == produto.Id);
            if (existente != null)
            {
                existente.Quantidade++;
            }
            else
            {
                produtosPedido.Add(new ProdutoPedido
                {
                    ProdutoId = produto.Id,
                    Produto = produto,
                    Quantidade = 1,
                    PrecoUnitario = produto.Preco
                });
            }
            AtualizarTotal();
        }

        protected void RemoverProduto(ProdutoPedido item)
        {
            produtosPedido.Remove(item);
            AtualizarTotal();
        }

        protected void AtualizarCarrinho()
        {
            AtualizarTotal();
        }

        protected void AtualizarTotal()
        {
            novoPedido.Total = produtosPedido.Sum(p => p.Quantidade * p.PrecoUnitario);
        }

        protected async Task AdicionarPedidoAsync()
        {
            if (produtosPedido.Count == 0 || novoPedido.ClienteId == 0)
            {
                Console.WriteLine("Erro: Preencha todos os campos.");
                return;
            }

            await PedidoService.AdicionarAsync(novoPedido, produtosPedido);
            Navigation.NavigateTo("/Pedidos");
        }
    }
}
