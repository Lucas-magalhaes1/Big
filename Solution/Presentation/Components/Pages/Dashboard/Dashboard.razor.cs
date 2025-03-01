using Microsoft.AspNetCore.Components;
using System.Globalization;
using MudBlazor;

namespace Big.Pages.Dashboard
{
    public class DashboardBase : ComponentBase
    {
        protected int mesSelecionado = DateTime.Now.Month;
        protected int anoSelecionado = DateTime.Now.Year;
        protected List<PedidoMock> mockPedidos = new();
        protected List<ChartSeries> seriesVendas = new();
        protected List<ChartSeries> seriesProdutos = new();
        protected List<ChartSeries> seriesVendedores = new();
        protected string[] labelsMeses = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[..12];
        protected string[] labelsProdutos = { "Produto A", "Produto B", "Produto C", "Produto D", "Produto E" };
        protected double[] dadosStatus;
        protected string[] labelsStatus = { "Aprovado", "Rejeitado", "Aguardando" };

        protected override async Task OnInitializedAsync()
        {
            GerarDadosMock();
        }

        protected void GerarDadosMock()
        {
            var random = new Random();
            mockPedidos = new List<PedidoMock>
            {
                new() { Id = 1001, ClienteNome = "Lucas Ferreira", DataPedido = new DateTime(anoSelecionado, mesSelecionado, 1), Status = "Aprovado", Total = random.Next(100, 800) },
                new() { Id = 1002, ClienteNome = "Ana Souza", DataPedido = new DateTime(anoSelecionado, mesSelecionado, 3), Status = "Rejeitado", Total = random.Next(100, 800) },
                new() { Id = 1003, ClienteNome = "Pedro Lima", DataPedido = new DateTime(anoSelecionado, mesSelecionado, 5), Status = "Aprovado", Total = random.Next(100, 800) },
                new() { Id = 1004, ClienteNome = "Mariana Costa", DataPedido = new DateTime(anoSelecionado, mesSelecionado, 7), Status = "Aguardando", Total = random.Next(100, 800) },
                new() { Id = 1005, ClienteNome = "Carlos Mendes", DataPedido = new DateTime(anoSelecionado, mesSelecionado, 10), Status = "Aprovado", Total = random.Next(100, 800) },
            };

            seriesVendas = new() { new ChartSeries { Name = "Vendas", Data = Enumerable.Range(0, 12).Select(_ => (double)random.Next(5, 20)).ToArray() } };
            seriesProdutos = new() { new ChartSeries { Name = "Quantidade Vendida", Data = new double[] { 25, 40, 35, 20, 50 } } };
            seriesVendedores = new()
            {
                new ChartSeries { Name = "Vendedor A", Data = new double[] { 10, 20, 15, 25, 30, 35, 20, 15, 10 } },
                new ChartSeries { Name = "Vendedor B", Data = new double[] { 5, 10, 12, 18, 22, 28, 32, 38, 40 } }
            };

            dadosStatus = new double[]
            {
                mockPedidos.Count(p => p.Status == "Aprovado"),
                mockPedidos.Count(p => p.Status == "Rejeitado"),
                mockPedidos.Count(p => p.Status == "Aguardando")
            };
        }

        protected decimal MediaTicket => mockPedidos.Any() ? mockPedidos.Sum(p => p.Total) / mockPedidos.Count : 0;

        protected async Task AtualizarDados(ChangeEventArgs e)
        {
            GerarDadosMock();
        }

        protected class PedidoMock
        {
            public int Id { get; set; }
            public string ClienteNome { get; set; }
            public DateTime DataPedido { get; set; }
            public string Status { get; set; }
            public decimal Total { get; set; }
        }
    }
}
