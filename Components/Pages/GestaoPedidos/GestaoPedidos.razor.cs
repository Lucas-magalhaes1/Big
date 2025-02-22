using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.GestaoPedidos
{
    public class GestaoPedidosBase : ComponentBase
    {
        [Inject] protected PedidoService PedidoService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected List<Pedido>? pedidos;
        protected List<IGrouping<DateTime, Pedido>> pedidosAgrupados;
        protected FiltrosPedido filtros = new();

        protected List<string> vendedoresMock = new() { "João Silva", "Maria Oliveira", "Carlos Souza", "Ana Lima", "Pedro Ferreira" };
        protected Dictionary<int, string> vendedoresAssociados = new();

        protected override async Task OnInitializedAsync()
        {
            await CarregarPedidos();
        }

        protected async Task CarregarPedidos()
        {
            pedidos = await PedidoService.ObterTodosAsync() ?? new List<Pedido>();

            var random = new Random();
            foreach (var pedido in pedidos)
            {
                if (!vendedoresAssociados.ContainsKey(pedido.Id))
                {
                    vendedoresAssociados[pedido.Id] = vendedoresMock[random.Next(vendedoresMock.Count)];
                }
            }

            AplicarFiltros();
        }

        protected string ObterVendedorAleatorio(int pedidoId)
        {
            return vendedoresAssociados.ContainsKey(pedidoId) ? vendedoresAssociados[pedidoId] : "Não definido";
        }

        protected void AplicarFiltros()
        {
            var resultado = pedidos.Where(p =>
                (string.IsNullOrEmpty(filtros.Cliente) || p.Cliente.Nome.Contains(filtros.Cliente, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(filtros.Status) || p.Status == filtros.Status) &&
                (!filtros.DataPedido.HasValue || p.DataPedido.Date == filtros.DataPedido.Value.Date) &&
                (!filtros.ValorMinimo.HasValue || p.Total >= filtros.ValorMinimo)
            ).OrderByDescending(p => p.DataPedido).ToList();

            pedidosAgrupados = resultado.GroupBy(p => p.DataPedido.Date).ToList();
        }

        protected async Task AplicarFiltrosAsync()
        {
            AplicarFiltros();
        }

        protected async Task RemoverFiltros()
        {
            filtros = new FiltrosPedido();
            AplicarFiltros();
        }

        protected async Task AlterarStatus(Pedido pedido, string novoStatus)
        {
            await PedidoService.AtualizarStatusAsync(pedido.Id, novoStatus);
            pedido.Status = novoStatus;
            StateHasChanged();
        }

        protected async Task GerarNota(Pedido pedido)
        {
            Console.WriteLine($"Gerando nota para pedido #{pedido.Id}...");
        }

        protected class FiltrosPedido
        {
            public string? Cliente { get; set; }
            public string? Status { get; set; }
            public DateTime? DataPedido { get; set; }
            public decimal? ValorMinimo { get; set; }
        }
    }
}
