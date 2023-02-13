using MeuMercado.Enums;
using MeuMercado.Helper;
using System.ComponentModel.DataAnnotations;

namespace MeuMercado.Models
{
    public class UsuarioModel
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
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public virtual List<ProdutoModel> Produtos { get; set; }
        public virtual List<NotaFiscalModel> NotasFiscais { get; set; }

        public bool EmailValido(string email)
        {
            if (Email == email)
            {
                return true;
            }
            return false;
        }
        public bool LoginValido(string login)
        {
            if (Login == login)
            {
                return true;
            }
            return false;
        }
        public bool SenhaValida(string senha)
        {
            if (Senha == senha.GerarHash())
            {
                return true;
            }
            return false;
        }
        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }

        public void SetHash()
        {
            Senha = Senha.GerarHash();
        }
    }
}
