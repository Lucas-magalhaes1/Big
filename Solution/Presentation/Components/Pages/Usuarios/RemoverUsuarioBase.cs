using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;

namespace Big.Pages.Usuarios
{
    public class RemoverUsuarioBase : ComponentBase
    {
        [Inject] protected UsuarioService UsuarioService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        public bool ExibirModal { get; private set; } = false;
        public ApplicationUser? UsuarioSelecionado { get; private set; }

        public async Task AbrirModal(string id)
        {
            UsuarioSelecionado = await UsuarioService.ObterPorIdAsync(id);
            ExibirModal = true;
            StateHasChanged();
        }

        protected async Task ConfirmarRemocao()
        {
            if (UsuarioSelecionado != null)
            {
                await UsuarioService.RemoverAsync(UsuarioSelecionado.Id);
                ExibirModal = false;
                Navigation.NavigateTo("/Usuarios", true);
            }
        }

        protected void FecharModal()
        {
            ExibirModal = false;
        }
    }
}