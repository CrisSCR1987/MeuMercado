using MeuMercado.Models;

namespace MeuMercado.Repositorio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorId(int id);
        UsuarioModel BuscarPorEmailELogin(string email, string login);
        UsuarioModel BuscarPorEmail(string email);
        UsuarioModel BuscarPorLogin(string login);
        List<UsuarioModel> BuscarPorNome(string nome);
        List<UsuarioModel> BuscarTodos();
        UsuarioModel Criar(UsuarioModel usuario);
        UsuarioModel Editar(UsuarioModel usuario);
        UsuarioModel Excluir(UsuarioModel usuario);
        UsuarioModel RedefinirSenha(UsuarioModel usuario);
        UsuarioModel AlterarSenha(UsuarioModel usuario);
    }
}
