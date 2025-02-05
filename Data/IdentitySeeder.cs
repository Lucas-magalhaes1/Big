using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Big.Models;

namespace Big.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            
            string[] roles = { "Vendedor", "Escritorio" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            
            var escritorioAdmin = await userManager.FindByEmailAsync("escritorioadmin@email.com");
            if (escritorioAdmin == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "escritorioadmin@email.com",
                    Email = "escritorioadmin@email.com",
                    NomeCompleto = "Administrador Escritório",
                    Endereco = "Av. Central, 123, Cidade X",
                    CPF = "123.456.789-00",
                    DataNascimento = new DateTime(1985, 7, 15),
                    FotoPerfilUrl = "https://via.placeholder.com/150",
                    EmailConfirmed = true,
                    IsAdmin = true, 
                    RoleAdicional = "Gestão Financeira"
                };
                var result = await userManager.CreateAsync(user, "Escritorio@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Escritorio");
                }
            }

            
            var vendedor = await userManager.FindByEmailAsync("vendedor@email.com");
            if (vendedor == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "vendedor@email.com",
                    Email = "vendedor@email.com",
                    NomeCompleto = "Vendedor Teste",
                    Endereco = "Rua das Vendas, 50, Bairro Y",
                    CPF = "987.654.321-00",
                    DataNascimento = new DateTime(1992, 5, 20),
                    FotoPerfilUrl = "https://via.placeholder.com/150",
                    EmailConfirmed = true,
                    IsAdmin = false, 
                    RoleAdicional = "Atendimento ao Cliente"
                };
                var result = await userManager.CreateAsync(user, "Vendedor@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Vendedor");
                }
            }
        }
    }
}
