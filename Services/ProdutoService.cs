using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Big.Data;
using Microsoft.EntityFrameworkCore;
using Big.Models;

namespace Big.Services
{
    public class ProdutoService
    {
        private readonly ApplicationDbContext _context;

        public ProdutoService(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<List<Produto>> ObterTodosAsync()
        {
            return await _context.Set<Produto>().ToListAsync();
        }

        
        public async Task<Produto> ObterPorIdAsync(int id)
        {
            var produto = await _context.Set<Produto>().FindAsync(id);
            return produto ?? throw new KeyNotFoundException("Produto não encontrado.");
        }

        
        public async Task AdicionarAsync(Produto produto)
        {
            await VerificarUrlImagem(produto.ImagemUrl);
            await _context.Set<Produto>().AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        
        public async Task AtualizarAsync(Produto produto)
        {
            await VerificarUrlImagem(produto.ImagemUrl);
            _context.Set<Produto>().Update(produto);
            await _context.SaveChangesAsync();
        }

        
        public async Task ExcluirAsync(int id)
        {
            var produto = await ObterPorIdAsync(id);
            if (produto != null)
            {
                _context.Set<Produto>().Remove(produto);
                await _context.SaveChangesAsync();
            }
        }

        
        public async Task<List<Produto>> BuscarComFiltrosAsync(string nome, decimal? precoMin, decimal? precoMax)
        {
            var query = _context.Set<Produto>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(p => p.Nome.Contains(nome));
            
            if (precoMin.HasValue)
                query = query.Where(p => p.Preco >= precoMin.Value);

            if (precoMax.HasValue)
                query = query.Where(p => p.Preco <= precoMax.Value);

            return await query.ToListAsync();
        }

        
        public async Task AtualizarEstoqueAsync(int id, int estoqueLoja, int estoqueCentroDistribuicao)
        {
            var produto = await ObterPorIdAsync(id);
            if (produto != null)
            {
                produto.EstoqueLoja = estoqueLoja;
                produto.EstoqueCentroDistribuicao = estoqueCentroDistribuicao;
                await AtualizarAsync(produto);
            }
        }
        
        private async Task VerificarUrlImagem(string imagemUrl)
        {
            if (string.IsNullOrWhiteSpace(imagemUrl))
            {
                throw new ArgumentException("A URL da imagem não pode estar vazia.");
            }

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(imagemUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("A URL da imagem é inválida ou inacessível.");
            }
        }
        public async Task<Produto> ObterPorIdComCategoriaAsync(int id)
        {
            return await _context.Produtos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);
        }
        
        public async Task<List<Produto>> BuscarPorCategoriaAsync(int categoriaId)
        {
            return await _context.Produtos
                .Where(p => p.CategoriaId == categoriaId)
                .Include(p => p.Categoria)
                .ToListAsync();
        }
    }
}
