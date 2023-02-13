using System.ComponentModel.DataAnnotations;

namespace MeuMercado.Models
{
    public class CriarProdutoModel
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
    }
}
