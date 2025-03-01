using Microsoft.AspNetCore.Components;
using Big.Models;

namespace Big.Pages.Vendedores
{
    public class VendedoresBase : ComponentBase
    {
        protected List<ApplicationUser> usuarios = new();
        protected List<ApplicationUser> usuariosFiltrados = new();
        protected string criterioOrdenacao = "nome";
        protected FiltrosUsuario filtros = new();

        protected override void OnInitialized()
        {
            CarregarUsuariosMock();
            AplicarFiltros();
        }

        private void CarregarUsuariosMock()
        {
            usuarios = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", NomeCompleto = "Lucas Ferreira", Email = "lucas@email.com", CPF = "123.456.789-00", RoleAdicional = "Gerente",UltimoLogin = DateTime.Now },
                new ApplicationUser { Id = "2", NomeCompleto = "Ana Souza", Email = "ana@email.com", CPF = "987.654.321-00", RoleAdicional = "Vendedor", UltimoLogin = DateTime.Now.AddHours(-1) },
                new ApplicationUser { Id = "3", NomeCompleto = "Pedro Lima", Email = "pedro@email.com", CPF = "654.321.987-00", RoleAdicional = "Atendente", UltimoLogin = DateTime.Now.AddDays(-1) },
                new ApplicationUser { Id = "4", NomeCompleto = "Mariana Costa", Email = "mariana@email.com", CPF = "321.987.654-00", RoleAdicional = "Financeiro", UltimoLogin = DateTime.Now.AddMinutes(-5) },
                new ApplicationUser { Id = "5", NomeCompleto = "Carlos Mendes", Email = "carlos@email.com", CPF = "111.222.333-44", RoleAdicional = "TI", UltimoLogin = DateTime.Now.AddMonths(-1) }
            };
        }

        protected void AplicarFiltros()
        {
            usuariosFiltrados = usuarios
                .Where(u => string.IsNullOrEmpty(filtros.Nome) || u.NomeCompleto.Contains(filtros.Nome, StringComparison.OrdinalIgnoreCase))
                .Where(u => string.IsNullOrEmpty(filtros.Email) || u.Email.Contains(filtros.Email, StringComparison.OrdinalIgnoreCase))
                .Where(u => string.IsNullOrEmpty(filtros.CPF) || u.CPF == filtros.CPF)
                .Where(u => string.IsNullOrEmpty(filtros.Cargo) || u.RoleAdicional == filtros.Cargo)
                .ToList();

            OrdenarUsuarios();
        }

        protected void OrdenarUsuarios()
        {
            switch (criterioOrdenacao)
            {
                case "nome":
                    usuariosFiltrados = usuariosFiltrados.OrderBy(u => u.NomeCompleto).ToList();
                    break;
                case "cadastro":
                    usuariosFiltrados = usuariosFiltrados.OrderBy(u => u.Id).ToList();
                    break;
                case "login":
                    usuariosFiltrados = usuariosFiltrados.OrderByDescending(u => u.UltimoLogin).ToList();
                    break;
            }
        }

        protected void RemoverUsuario(string id)
        {
            usuarios.RemoveAll(u => u.Id == id);
            AplicarFiltros();
        }

        protected void BloquearUsuario(string id)
        {
            Console.WriteLine($"Usuário {id} bloqueado.");
        }

        protected void RemoverFiltros()
        {
            filtros = new FiltrosUsuario();
            AplicarFiltros();
        }

        protected class FiltrosUsuario
        {
            public string? Nome { get; set; }
            public string? Email { get; set; }
            public string? CPF { get; set; }
            public string? Cargo { get; set; }
        }
    }
}
