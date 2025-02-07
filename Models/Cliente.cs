using System.ComponentModel.DataAnnotations;

namespace Big.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "O email informado não é válido.")]
        [StringLength(150, ErrorMessage = "O email deve ter no máximo 150 caracteres.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "O telefone informado não é válido.")]
        [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres.")]
        public string Telefone { get; set; }

        [StringLength(200, ErrorMessage = "O endereço deve ter no máximo 200 caracteres.")]
        public string Endereco { get; set; }

        [StringLength(14, ErrorMessage = "O CPF ou CNPJ deve ter no máximo 14 caracteres.")]
        public string Documento { get; set; } 

        [DataType(DataType.Date)]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}