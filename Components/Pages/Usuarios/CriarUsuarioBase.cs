using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Usuarios
{
    public class CriarUsuarioBase : ComponentBase
    {
        [Inject] protected UsuarioService UsuarioService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected ApplicationUser novoUsuario = new ApplicationUser();

        protected async Task OnValidSubmitAsync()
        {
            try
            {
                await UsuarioService.AtualizarAsync(novoUsuario);
                Navigation.NavigateTo("/Usuarios");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar o usuário: {ex.Message}");
            }
        }
    }
}