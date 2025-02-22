using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Big.Models;

namespace Big.Services
{
    public class VendedorService
    {
        private readonly List<ApplicationUser> _vendedores = new()
        {
            new ApplicationUser { Id = "1", NomeCompleto = "Lucas Ferreira", Email = "lucas@email.com", CPF = "123.456.789-00", RoleAdicional = "Gerente", UltimoLogin = DateTime.Now },
            new ApplicationUser { Id = "2", NomeCompleto = "Ana Souza", Email = "ana@email.com", CPF = "987.654.321-00", RoleAdicional = "Vendedor", UltimoLogin = DateTime.Now.AddHours(-1) },
            new ApplicationUser { Id = "3", NomeCompleto = "Pedro Lima", Email = "pedro@email.com", CPF = "654.321.987-00", RoleAdicional = "Atendente", UltimoLogin = DateTime.Now.AddDays(-1) },
            new ApplicationUser { Id = "4", NomeCompleto = "Mariana Costa", Email = "mariana@email.com", CPF = "321.987.654-00", RoleAdicional = "Financeiro", UltimoLogin = DateTime.Now.AddMinutes(-5) },
            new ApplicationUser { Id = "5", NomeCompleto = "Carlos Mendes", Email = "carlos@email.com", CPF = "111.222.333-44", RoleAdicional = "TI", UltimoLogin = DateTime.Now.AddMonths(-1) }
        };

        public async Task<List<ApplicationUser>> ObterTodosAsync()
        {
            await Task.Delay(100); 
            return _vendedores.ToList();
        }

        public async Task<ApplicationUser?> ObterPorIdAsync(string id)
        {
            await Task.Delay(50);
            return _vendedores.FirstOrDefault(u => u.Id == id);
        }

        public async Task<List<ApplicationUser>> AplicarFiltrosAsync(FiltrosUsuario filtros)
        {
            await Task.Delay(50);
            return _vendedores
                .Where(u => string.IsNullOrEmpty(filtros.Nome) || u.NomeCompleto.Contains(filtros.Nome, StringComparison.OrdinalIgnoreCase))
                .Where(u => string.IsNullOrEmpty(filtros.Email) || u.Email.Contains(filtros.Email, StringComparison.OrdinalIgnoreCase))
                .Where(u => string.IsNullOrEmpty(filtros.CPF) || u.CPF == filtros.CPF)
                .Where(u => string.IsNullOrEmpty(filtros.Cargo) || u.RoleAdicional == filtros.Cargo)
                .ToList();
        }

        public async Task<List<ApplicationUser>> OrdenarAsync(List<ApplicationUser> usuarios, string criterio)
        {
            await Task.Delay(50);
            return criterio switch
            {
                "nome" => usuarios.OrderBy(u => u.NomeCompleto).ToList(),
                "cadastro" => usuarios.OrderBy(u => u.Id).ToList(),
                "login" => usuarios.OrderByDescending(u => u.UltimoLogin).ToList(),
                _ => usuarios
            };
        }

        public async Task AtualizarAsync(ApplicationUser usuario)
        {
            await Task.Delay(50);
            var index = _vendedores.FindIndex(u => u.Id == usuario.Id);
            if (index != -1)
            {
                _vendedores[index] = usuario;
            }
        }

        public async Task RemoverAsync(string id)
        {
            await Task.Delay(50);
            _vendedores.RemoveAll(u => u.Id == id);
        }
    }

    public class FiltrosUsuario
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? CPF { get; set; }
        public string? Cargo { get; set; }
    }
}
