using System.ComponentModel.DataAnnotations;

namespace MeuMercado.Models
{
    public class AlterarSenhaModel
    {
        [Required(ErrorMessage = "A senha atual é obrigatória.")]
        public string SenhaAtual { get; set; }
        [Required(ErrorMessage = "A Nova Senha é obrigatória.")]
        public string NovaSenha { get; set; }
        [Required(ErrorMessage = "Confirmar Nova Senha é obrigatória")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
