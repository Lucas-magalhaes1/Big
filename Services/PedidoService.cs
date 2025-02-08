using Big.Data;
using Microsoft.EntityFrameworkCore; // Certifique-se de importar o contexto correto

namespace Big.Services
{
    public class PedidoService
    {
        private readonly ApplicationDbContext _context;

        public PedidoService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obter todos os pedidos com os detalhes do cliente e produtos
        public async Task<List<Pedido>> ObterTodosAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Produtos)
                .ThenInclude(pp => pp.Produto) // Inclui os produtos do pedido
                .ToListAsync();
        }

        // Obter um pedido específico pelo ID
        public async Task<Pedido> ObterPorIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Produtos)
                .ThenInclude(pp => pp.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Criar um novo pedido com os produtos associados
        public async Task AdicionarAsync(Pedido pedido, List<ProdutoPedido> produtosPedido)
        {
            // Adiciona o pedido ao banco de dados
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            // Associa os produtos ao pedido
            foreach (var item in produtosPedido)
            {
                item.PedidoId = pedido.Id; // Vincula ao pedido recém-criado
                _context.ProdutosPedidos.Add(item);
            }

            await _context.SaveChangesAsync();
        }

        // Atualizar um pedido existente
        public async Task AtualizarAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        // Excluir um pedido e seus produtos associados
        public async Task ExcluirAsync(int id)
        {
            var pedido = await ObterPorIdAsync(id);
            if (pedido != null)
            {
                _context.ProdutosPedidos.RemoveRange(pedido.Produtos); // Remove os produtos do pedido
                _context.Pedidos.Remove(pedido); // Remove o pedido
                await _context.SaveChangesAsync();
            }
        }

        // Aprovar ou rejeitar um pedido (Status = "Aprovado" ou "Rejeitado")
        public async Task AtualizarStatusAsync(int pedidoId, string novoStatus)
        {
            var pedido = await _context.Pedidos.FindAsync(pedidoId);
            if (pedido != null)
            {
                pedido.Status = novoStatus;
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<List<Pedido>> ObterPedidosPorClienteAsync(int clienteId, int quantidade = 5)
        {
            return await _context.Pedidos
                .Where(p => p.ClienteId == clienteId)
                .OrderByDescending(p => p.DataPedido) // Ordena pelos mais recentes
                .Take(quantidade) // Limita ao número desejado
                .Include(p => p.Produtos)
                .ThenInclude(pp => pp.Produto) // Inclui os produtos dos pedidos
                .ToListAsync();
        }

    }
}
