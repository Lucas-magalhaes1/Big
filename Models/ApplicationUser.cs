using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Big.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome completo deve ter no máximo 100 caracteres.")]
        public string NomeCompleto { get; set; }

        [StringLength(200, ErrorMessage = "O endereço deve ter no máximo 200 caracteres.")]
        public string? Endereco { get; set; }

        [StringLength(15, ErrorMessage = "O CPF deve ter no máximo 15 caracteres.")]
        public string? CPF { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataNascimento { get; set; }

        [StringLength(300, ErrorMessage = "A URL da foto deve ter no máximo 300 caracteres.")]
        public string? FotoPerfilUrl { get; set; }
        
        public bool IsAdmin { get; set; } = false; 
        public string? RoleAdicional { get; set; }
        
        public DateTime? UltimoLogin { get; set; }
    }
}