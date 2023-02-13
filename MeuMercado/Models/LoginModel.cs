using System.ComponentModel.DataAnnotations;

namespace MeuMercado.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O Login é obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "A Senha é obrigatória")]
        public string Senha { get; set; }
    }
}
