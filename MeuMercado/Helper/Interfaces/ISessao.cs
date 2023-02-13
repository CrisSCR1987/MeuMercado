using MeuMercado.Models;

namespace MeuMercado.Helper.Interfaces
{
    public interface ISessao
    {
        UsuarioModel BuscarSessaoDoUsuario();
        void CriarSessaoDoUsuario(UsuarioModel usuario);
        void RemoverSessaoDoUsuario();
    }
}
