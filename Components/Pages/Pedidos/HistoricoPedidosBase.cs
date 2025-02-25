using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Clientes
{
    public class HistoricoPedidosBase : ComponentBase
    {
        [Parameter] public int id { get; set; }

        [Inject] protected PedidoService PedidoService { get; set; } = default!;
        [Inject] protected ClienteService ClienteService { get; set; } = default!;
        
        [Inject] protected NavigationManager Navigation { get; set; } = default!;


        protected Cliente? cliente;
        protected List<Pedido> pedidos = new();

        protected override async Task OnInitializedAsync()
        {
            cliente = await ClienteService.ObterPorIdAsync(id);
            pedidos = await PedidoService.ObterPorClienteIdAsync(id);
        }
        
        protected async Task DuplicarPedido(int pedidoId)
        {
            var pedidoOriginal = await PedidoService.ObterPorIdAsync(pedidoId);
            if (pedidoOriginal == null) return;

            var novoPedido = new Pedido
            {
                ClienteId = pedidoOriginal.ClienteId,
                DataPedido = DateTime.Now, 
                Status = "Aguardando", 
                FormaPagamento = pedidoOriginal.FormaPagamento,
                Total = pedidoOriginal.Produtos.Sum(p => p.Quantidade * p.PrecoUnitario), 
                Produtos = pedidoOriginal.Produtos.Select(p => new ProdutoPedido
                {
                    ProdutoId = p.ProdutoId,
                    Quantidade = p.Quantidade,
                    PrecoUnitario = p.PrecoUnitario
                }).ToList()
            };

            var novoPedidoId = await PedidoService.AdicionarERetornarIdAsync(novoPedido);
            Navigation.NavigateTo($"/Pedidos/Editar/{novoPedidoId}"); 
        }
    }
}