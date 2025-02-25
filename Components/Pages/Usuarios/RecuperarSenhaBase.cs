using Microsoft.AspNetCore.Components;

namespace Big.Pages
{
    public class RecuperarSenhaBase : ComponentBase
    {
        [Inject] protected NavigationManager Navigation { get; set; } = default!;

        protected void Voltar()
        {
            Navigation.NavigateTo("/Login");
        }
    }
}