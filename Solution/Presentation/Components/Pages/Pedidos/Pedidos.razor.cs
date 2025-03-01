using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Pedidos
{
    public class PedidosBase : ComponentBase
    {
        [Inject] protected PedidoService PedidoService { get; set; }

        protected List<Pedido>? pedidos;
        protected Dictionary<DateTime, List<Pedido>> pedidosAgrupados = new();
        protected Pedido? pedidoParaExcluir;
        protected FiltroPedido filtros = new();

        protected override async Task OnInitializedAsync()
        {
            await CarregarPedidos();
        }

        protected async Task CarregarPedidos()
        {
            var todosPedidos = await PedidoService.ObterTodosAsync();
            pedidos = AplicarFiltros(todosPedidos);

            pedidosAgrupados = pedidos
                .GroupBy(p => p.DataPedido.Date)
                .OrderByDescending(g => g.Key)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        protected List<Pedido> AplicarFiltros(List<Pedido> todosPedidos)
        {
            return todosPedidos
                .Where(p =>
                    (string.IsNullOrEmpty(filtros.NomeCliente) || (p.Cliente != null && p.Cliente.Nome.Contains(filtros.NomeCliente, StringComparison.OrdinalIgnoreCase))) &&
                    (!filtros.DataInicial.HasValue || p.DataPedido.Date >= filtros.DataInicial.Value.Date) &&
                    (!filtros.DataFinal.HasValue || p.DataPedido.Date <= filtros.DataFinal.Value.Date) &&
                    (string.IsNullOrEmpty(filtros.Status) || p.Status == filtros.Status) &&
                    (!filtros.ValorMinimo.HasValue || p.Total >= filtros.ValorMinimo.Value) &&
                    (!filtros.ValorMaximo.HasValue || p.Total <= filtros.ValorMaximo.Value))
                .ToList();
        }

        protected async Task AplicarFiltrosAsync()
        {
            await CarregarPedidos();
        }

        protected async Task RemoverFiltros()
        {
            filtros = new FiltroPedido();
            await CarregarPedidos();
        }

        protected void ConfirmarExclusao(int pedidoId)
        {
            pedidoParaExcluir = pedidos?.FirstOrDefault(p => p.Id == pedidoId);
        }

        protected async Task ExcluirPedido()
        {
            if (pedidoParaExcluir != null)
            {
                await PedidoService.ExcluirAsync(pedidoParaExcluir.Id);
                await CarregarPedidos();
                pedidoParaExcluir = null;
            }
        }

        protected void CancelarExclusao()
        {
            pedidoParaExcluir = null;
        }

        protected class FiltroPedido
        {
            public string? NomeCliente { get; set; }
            public DateTime? DataInicial { get; set; }
            public DateTime? DataFinal { get; set; }
            public string? Status { get; set; }
            public decimal? ValorMinimo { get; set; }
            public decimal? ValorMaximo { get; set; }
        }
    }
}
