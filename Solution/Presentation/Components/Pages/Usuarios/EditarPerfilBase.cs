using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Big.Pages.Perfil
{
    public class EditarPerfilBase : ComponentBase
    {
        [Inject] protected UsuarioService UsuarioService { get; set; } = default!;
        [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        protected ApplicationUser? usuario;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var userEmail = authState.User.Identity?.Name;

            if (!string.IsNullOrEmpty(userEmail))
            {
                usuario = await UsuarioService.ObterPorEmailAsync(userEmail);
            }
        }

        protected async Task SalvarAlteracoes()
        {
            if (usuario != null)
            {
                await UsuarioService.AtualizarAsync(usuario);
                Navigation.NavigateTo("/Perfil/Editar");
            }
        }
    }
}