using Big.Data;
using Microsoft.EntityFrameworkCore;

namespace Big.Services
{
    public class PedidoService
    {
        private readonly ApplicationDbContext _context;

        public PedidoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> ObterTodosAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Produtos)
                .ThenInclude(pp => pp.Produto)
                .ToListAsync();
        }

        public async Task<Pedido> ObterPorIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Produtos)
                .ThenInclude(pp => pp.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<List<Pedido>> ObterPorClienteIdAsync(int clienteId)
        {
            return await _context.Pedidos
                .Where(p => p.ClienteId == clienteId)
                .OrderByDescending(p => p.DataPedido)
                .ToListAsync();
        }


        public async Task AdicionarAsync(Pedido pedido, List<ProdutoPedido> produtosPedido)
        {
            pedido.Total = produtosPedido.Sum(p => p.Quantidade * p.PrecoUnitario);

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            foreach (var item in produtosPedido)
            {
                item.PedidoId = pedido.Id;
                _context.ProdutosPedidos.Add(item);
            }

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            var pedido = await ObterPorIdAsync(id);
            if (pedido != null)
            {
                _context.ProdutosPedidos.RemoveRange(pedido.Produtos);
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AtualizarStatusAsync(int pedidoId, string novoStatus)
        {
            var pedido = await _context.Pedidos.FindAsync(pedidoId);
            if (pedido != null)
            {
                pedido.Status = novoStatus;
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<int> AdicionarERetornarIdAsync(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido.Id; 
        }

    }
}
