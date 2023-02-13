using MeuMercado.Data;
using MeuMercado.Helper;
using MeuMercado.Models;
using MeuMercado.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeuMercado.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;

        public UsuarioRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public UsuarioModel BuscarPorEmailELogin(string email, string login)
        {
            UsuarioModel usuario = _bancoContext.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());
            if (usuario == null)
            {
                throw new Exception("Email e/ou Login incorretos.");
            }
            return usuario;
        }
        public UsuarioModel BuscarPorEmail(string email)
        {
            UsuarioModel usuario = _bancoContext.Usuarios.FirstOrDefault(x => x.Email == email);
            return usuario;
        }
        public UsuarioModel BuscarPorId(int id)
        {
            UsuarioModel usuario = _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
            return usuario;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            UsuarioModel usuario = _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
            return usuario;
        }

        public List<UsuarioModel> BuscarPorNome(string nome)
        {
            return _bancoContext.Usuarios.Where(x => x.Nome == nome).ToList();
            
        }

        public List<UsuarioModel> BuscarTodos()
        {
           return _bancoContext.Usuarios.Include(x => x.Produtos).ToList();
        }

        public UsuarioModel Criar(UsuarioModel usuario)
        {
            UsuarioModel usuarioEmail = BuscarPorEmail(usuario.Email);
            UsuarioModel usuarioLogin = BuscarPorLogin(usuario.Login);
             if (usuarioEmail != null || usuarioLogin != null)
            {
                throw new Exception("Email e/ou login já existem.");
            }
            usuario.DataCadastro = DateTime.Now;
            usuario.SetHash();
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel Editar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = BuscarPorId(usuario.Id);
            if (usuarioDB == null)
            {
                throw new Exception("Usuário não encontrado pelo Id.");
            }
            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Sexo = usuario.Sexo;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Email = usuario.Email;
            usuarioDB.DataAtualizacao = DateTime.Now;
            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();
            return usuarioDB;
        }

        public UsuarioModel Excluir(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = BuscarPorId(usuario.Id);
            if (usuarioDB == null)
            {
                throw new Exception("Não foi possível excluir o usuário, pois o ID do próprio não consta na nossa base de dados");
            }
            _bancoContext.Usuarios.Remove(usuarioDB);
            _bancoContext.SaveChanges();
            return usuarioDB;
        }

        public UsuarioModel RedefinirSenha(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = BuscarPorId(usuario.Id);
            if (usuarioDB == null)
            {
                throw new Exception("Usuário não encontrado pelo ID.");
            }
            usuarioDB.Senha = usuario.Senha;
            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();
            return usuarioDB;
        }

        public UsuarioModel AlterarSenha(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = BuscarPorId(usuario.Id);
            if (usuarioDB == null)
            {
                throw new Exception("Não foi possível encontrar o Usuário pelo ID.");
            }
            usuarioDB.Senha = usuario.Senha;
            usuarioDB.SetHash();
            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();
            return usuarioDB;
        }
    }
}
