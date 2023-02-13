namespace MeuMercado.Models
{
    public class NotaFiscalModel
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public int UsuarioId { get; set; }
        public int ProdutoId { get; set; }
        public virtual UsuarioModel Usuario { get; set; }
        public virtual ProdutoModel Produto { get; set; }
    }
}
