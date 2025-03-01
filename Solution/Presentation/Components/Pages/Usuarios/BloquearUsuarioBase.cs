using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Usuarios
{
    public class BloquearUsuarioBase : ComponentBase
    {
        [Inject] protected UsuarioService UsuarioService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        [Parameter] public string Id { get; set; } = default!;
        protected ApplicationUser? usuario;

        protected override async Task OnInitializedAsync()
        {
            usuario = await UsuarioService.ObterPorIdAsync(Id);
        }

        protected async Task BloquearUsuario()
        {
            if (usuario != null)
            {
                Console.WriteLine($"Usuário {usuario.Id} bloqueado."); 
                await UsuarioService.AtualizarAsync(usuario); 
                Navigation.NavigateTo("/Usuarios");
            }
        }
    }
}