using MeuMercado.Models;

namespace MeuMercado.Repositorio.Interfaces
{
    public interface IProdutoRepositorio
    {
        ProdutoModel BuscarPorId(int id);
        List<ProdutoModel> BuscarPorNome(string nome);
        List<ProdutoModel> BuscarTodos();
        List<ProdutoModel> BuscarTodosDoUsuario(int id);
        List<ProdutoModel> BuscarPorCategoria(string categoria);
        ProdutoModel Criar(ProdutoModel produto);
        ProdutoModel Editar(ProdutoModel produto);
        ProdutoModel Excluir(int id);
        
    }
}
