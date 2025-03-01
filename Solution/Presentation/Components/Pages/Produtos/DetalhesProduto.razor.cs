using Microsoft.AspNetCore.Components;
using Big.Models;
using Big.Services;
using System.Globalization;

namespace Big.Pages.Produtos
{
    public class DetalhesProdutoBase : ComponentBase
    {
        [Parameter] public int Id { get; set; }

        [Inject] protected ProdutoService ProdutoService { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }

        protected Produto produto;

        protected override async Task OnInitializedAsync()
        {
            produto = await ProdutoService.ObterPorIdComCategoriaAsync(Id);
        }

        protected void Voltar()
        {
            Navigation.NavigateTo("/Produtos");
        }
    }
}