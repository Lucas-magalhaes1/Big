using Microsoft.AspNetCore.Components;
using Big.Models;

namespace Big.Pages.Vendedores
{
    public class EditarVendedorBase : ComponentBase
    {
        [Parameter] public string id { get; set; }

        [Inject] protected NavigationManager Navigation { get; set; }

        protected ApplicationUser usuario;

        protected override void OnInitialized()
        {
            usuario = new ApplicationUser
            {
                Id = id,
                NomeCompleto = "Usuário Mockado",
                Email = "usuario@email.com",
                CPF = "000.000.000-00",
                Endereco = "Rua Exemplo, 123",
                RoleAdicional = "Vendedor",
            };
        }

        protected void SalvarAlteracoes()
        {
            Console.WriteLine("Usuário atualizado!");
            Navigation.NavigateTo("/Usuarios");
        }

        protected void ResetarSenha()
        {
            Console.WriteLine("Senha resetada!");
        }
    }
}