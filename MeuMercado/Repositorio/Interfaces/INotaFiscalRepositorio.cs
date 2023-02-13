using MeuMercado.Models;

namespace MeuMercado.Repositorio.Interfaces
{
    public interface INotaFiscalRepositorio
    {
        NotaFiscalModel BuscarPorId(int id);
        List<NotaFiscalModel> BuscarTodosDoUsuario(int usuarioId);
        NotaFiscalModel Criar(NotaFiscalModel notaFiscal);
    }
}
