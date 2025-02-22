using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Big.Models;

namespace Big.Services
{
    public class DashboardService
    {
        private readonly Random _random = new();

        public async Task<List<Pedido>> ObterPedidosAsync(int mes, int ano)
        {
            await Task.Delay(100); 
            return GerarPedidosMock(mes, ano);
        }

        public async Task<List<ChartSeriesData>> ObterEvolucaoVendasAsync()
        {
            await Task.Delay(100);
            return new List<ChartSeriesData>
            {
                new ChartSeriesData("Vendas",
                    Enumerable.Range(0, 12).Select(_ => (double)_random.Next(5, 20)).ToArray())
            };
        }

        public async Task<List<ChartSeriesData>> ObterProdutosMaisVendidosAsync()
        {
            await Task.Delay(100);
            return new List<ChartSeriesData>
            {
                new ChartSeriesData("Quantidade Vendida", new double[] { 25, 40, 35, 20, 50 })
            };
        }

        public async Task<List<ChartSeriesData>> ObterVendasPorVendedorAsync()
        {
            await Task.Delay(100);
            return new List<ChartSeriesData>
            {
                new ChartSeriesData("Vendedor A", new double[] { 10, 20, 15, 25, 30, 35, 20, 15, 10 }),
                new ChartSeriesData("Vendedor B", new double[] { 5, 10, 12, 18, 22, 28, 32, 38, 40 })
            };
        }

        public async Task<(double[] dados, string[] labels)> ObterPedidosPorStatusAsync(List<Pedido> pedidos)
        {
            await Task.Delay(100);
            return (
                new double[]
                {
                    pedidos.Count(p => p.Status == "Aprovado"),
                    pedidos.Count(p => p.Status == "Rejeitado"),
                    pedidos.Count(p => p.Status == "Aguardando")
                },
                new string[] { "Aprovado", "Rejeitado", "Aguardando" }
            );
        }

        public decimal CalcularTicketMedio(List<Pedido> pedidos)
        {
            return pedidos.Count > 0 ? pedidos.Sum(p => p.Total) / pedidos.Count : 0;
        }

        private List<Pedido> GerarPedidosMock(int mes, int ano)
        {
            return new List<Pedido>
            {
                new Pedido
                {
                    Id = 1001, Cliente = new Cliente { Nome = "Lucas Ferreira" },
                    DataPedido = new DateTime(ano, mes, 1), Status = "Aprovado", Total = _random.Next(100, 800)
                },
                new Pedido
                {
                    Id = 1002, Cliente = new Cliente { Nome = "Ana Souza" }, DataPedido = new DateTime(ano, mes, 3),
                    Status = "Rejeitado", Total = _random.Next(100, 800)
                },
                new Pedido
                {
                    Id = 1003, Cliente = new Cliente { Nome = "Pedro Lima" }, DataPedido = new DateTime(ano, mes, 5),
                    Status = "Aprovado", Total = _random.Next(100, 800)
                },
                new Pedido
                {
                    Id = 1004, Cliente = new Cliente { Nome = "Mariana Costa" }, DataPedido = new DateTime(ano, mes, 7),
                    Status = "Aguardando", Total = _random.Next(100, 800)
                },
                new Pedido
                {
                    Id = 1005, Cliente = new Cliente { Nome = "Carlos Mendes" },
                    DataPedido = new DateTime(ano, mes, 10), Status = "Aprovado", Total = _random.Next(100, 800)
                }
            };
        }
    }


    public class ChartSeriesData
    {
        public string Name { get; set; }
        public double[] Data { get; set; }

        public ChartSeriesData(string name, double[] data)
        {
            Name = name;
            Data = data;
        }
    }
}
