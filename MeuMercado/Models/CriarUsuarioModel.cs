using MeuMercado.Enums;
using System.ComponentModel.DataAnnotations;

namespace MeuMercado.Models
{
    public class CriarUsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O Sexo é obrigatório")]
        public SexoEnum Sexo { get; set; }
        [Required(ErrorMessage = "O Perfil é obrigatório")]
        public PerfilEnum Perfil { get; set; }
        [Required(ErrorMessage = "O Login é obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um email válido !")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Senha { get; set; }
    }
}
