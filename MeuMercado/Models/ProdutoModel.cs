using System.ComponentModel.DataAnnotations;

namespace MeuMercado.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A categoria é obrigatória")]
        public string Categoria { get; set; }
        [Required(ErrorMessage = "A quantidade é obrigatória")]
        public int Quantidade { get; set; }
        [Required(ErrorMessage = "O valor é obrigatório")]
        public double Valor { get; set; }
        public int UsuarioId { get; set; }
        public virtual UsuarioModel Usuario { get; set; }
        public virtual List<NotaFiscalModel> NotasFiscais { get; set; }
    }
}
