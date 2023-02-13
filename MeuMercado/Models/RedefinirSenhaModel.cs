using System.ComponentModel.DataAnnotations;

namespace MeuMercado.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "O Email é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O Login é obrigatório")]
        public string Login { get; set; }
    }
}
