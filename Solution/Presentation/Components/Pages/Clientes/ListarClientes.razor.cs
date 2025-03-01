using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Clientes
{
    public class ListarClientesBase : ComponentBase
    {
        [Inject] protected ClienteService ClienteService { get; set; }

        protected List<Cliente>? clientes;
        protected FiltroCliente filtros = new();
        protected int paginaAtual = 1;
        protected int totalPaginas = 1;
        protected const int tamanhoPagina = 10;
        protected bool exibirInativos = false;

        protected override async Task OnInitializedAsync()
        {
            await CarregarClientes();
        }

        protected async Task CarregarClientes()
        {
            var todosClientes = await ClienteService.ObterTodosAsync();
            clientes = todosClientes.Where(c => c.Ativo != exibirInativos).ToList();
            clientes = AplicarFiltros(clientes);
            totalPaginas = (int)Math.Ceiling((double)clientes.Count / tamanhoPagina);
            clientes = clientes.Skip((paginaAtual - 1) * tamanhoPagina).Take(tamanhoPagina).ToList();
        }

        protected List<Cliente> AplicarFiltros(List<Cliente> todosClientes)
        {
            return todosClientes
                .Where(c =>
                    (string.IsNullOrEmpty(filtros.Nome) || c.Nome.Contains(filtros.Nome, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrEmpty(filtros.Documento) || c.Documento.Contains(filtros.Documento)) &&
                    (string.IsNullOrEmpty(filtros.Endereco) || c.Endereco.Contains(filtros.Endereco, StringComparison.OrdinalIgnoreCase)) &&
                    (!filtros.DataCadastro.HasValue || c.DataCadastro.Date == filtros.DataCadastro.Value.Date))
                .ToList();
        }

        protected async Task AplicarFiltrosAsync() => await CarregarClientes();
        protected async Task RemoverFiltros() { filtros = new FiltroCliente(); await CarregarClientes(); }
        protected async Task MudarPagina(int pagina) { paginaAtual = pagina; await CarregarClientes(); }
        protected async Task AlternarStatus(Cliente cliente) { cliente.Ativo = !cliente.Ativo; await ClienteService.AtualizarAsync(cliente); await CarregarClientes(); }
        protected async Task AlternarExibicaoClientes() { exibirInativos = !exibirInativos; await CarregarClientes(); }
        protected async Task ExportarCsv() => Console.WriteLine("Exportando CSV...");
        protected async Task ExportarPdf() => Console.WriteLine("Exportando PDF...");

        public class FiltroCliente
        {
            public string? Nome { get; set; }
            public string? Documento { get; set; }
            public string? Endereco { get; set; }
            public DateTime? DataCadastro { get; set; }
        }
    }
}
