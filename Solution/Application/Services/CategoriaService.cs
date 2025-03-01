using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Big.Models;
using Big.Data;

namespace Big.Services
{
    public class CategoriaService
    {
        private readonly ApplicationDbContext _context;

        public CategoriaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> ObterTodasAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria> ObterPorIdAsync(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        public async Task AdicionarAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            var categoria = await ObterPorIdAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
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