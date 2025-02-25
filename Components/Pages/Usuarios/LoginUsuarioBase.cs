using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using Big.Services;

namespace Big.Pages.Login
{
    public class LoginBase : ComponentBase
    {
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected IJSRuntime JS { get; set; } = default!;

        protected LoginModel loginModel = new LoginModel();
        protected string? errorMessage;

        protected async Task Login()
        {
            errorMessage = null; 

            var token = await AuthService.LoginAsync(loginModel.Email, loginModel.Senha);
            if (string.IsNullOrEmpty(token))
            {
                errorMessage = "Email ou senha inválidos.";
                return;
            }
            
            await JS.InvokeVoidAsync("localStorage.setItem", "authToken", token);
            
            Navigation.NavigateTo("/");
        }

        protected class LoginModel
        {
            [Required(ErrorMessage = "O email é obrigatório.")]
            [EmailAddress(ErrorMessage = "Email inválido.")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "A senha é obrigatória.")]
            [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
            public string Senha { get; set; } = string.Empty;
        }
    }
}