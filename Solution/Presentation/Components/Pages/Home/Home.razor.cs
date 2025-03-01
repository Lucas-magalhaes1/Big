using MudBlazor;

public class HomeCode
{
    public List<PedidoMock> mockPedidos { get; private set; } = new();
    public List<ChartSeries> seriesVendas { get; private set; } = new();
    public string[] labelsMeses { get; } = { "Jul", "Ago", "Set", "Out", "Nov" };

    public void GerarDadosMock()
    {
        var random = new Random();
        mockPedidos = new List<PedidoMock>
        {
            new PedidoMock { ClienteNome = "Ana Souza", DataPedido = DateTime.Now, Status = "Aprovado", Total = random.Next(500, 1000) },
            new PedidoMock { ClienteNome = "Carlos Mendes", DataPedido = DateTime.Now, Status = "Rejeitado", Total = random.Next(50, 200) },
            new PedidoMock { ClienteNome = "Mariana Costa", DataPedido = DateTime.Now, Status = "Aprovado", Total = random.Next(600, 1200) },
            new PedidoMock { ClienteNome = "João Lima", DataPedido = DateTime.Now, Status = "Aprovado", Total = random.Next(700, 1300) },
            new PedidoMock { ClienteNome = "Pedro Silva", DataPedido = DateTime.Now, Status = "Rejeitado", Total = random.Next(50, 200) },
        };

        seriesVendas = new List<ChartSeries>
        {
            new ChartSeries { Name = "Pedidos Rejeitados", Data = new double[] { random.Next(1, 10), random.Next(1, 10), random.Next(1, 10), random.Next(1, 10), random.Next(1, 10) }, },
            new ChartSeries { Name = "Pedidos Aprovados", Data = new double[] { random.Next(30, 60), random.Next(30, 60), random.Next(30, 60), random.Next(30, 60), random.Next(30, 60) }, }
        };
    }

    public decimal MediaTicket => mockPedidos.Count > 0 ? mockPedidos.Sum(p => p.Total) / mockPedidos.Count : 0;
}

public class PedidoMock
{
    public string ClienteNome { get; set; }
    public DateTime DataPedido { get; set; }
    public string Status { get; set; }
    public decimal Total { get; set; }
}