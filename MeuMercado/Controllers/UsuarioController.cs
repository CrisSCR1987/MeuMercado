using MeuMercado.Filters;
using MeuMercado.Helper.Interfaces;
using MeuMercado.Models;
using MeuMercado.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeuMercado.Controllers
{
    [PaginaParaUsuarioLogadoEAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public UsuarioController(IUsuarioRepositorio usuariorepositorio, ISessao sessao)
        {
            _sessao = sessao;
            _usuarioRepositorio= usuariorepositorio;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
             if (usuario == null)
            {
                TempData["MensagemErro"] = "Não foi possível encontrar o usuário pelo ID.";
                return View("Index");
            }
             EditarUsuarioModel usuarioSemSenha = new EditarUsuarioModel()
             {
                 Id = usuario.Id,
                 Nome = usuario.Nome,
                 Sexo = usuario.Sexo,
                 Perfil = usuario.Perfil,
                 Email = usuario.Email,
                 Login = usuario.Login,
             };
             return View(usuarioSemSenha);
        }

        [HttpGet]
        public IActionResult ExcluirConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            if (usuario == null)
            {
                TempData["MensagemErro"] = "Não foi possível encontrar o usuário pelo ID.";
                return View("Index");
            }
            return View(usuario);
        }
        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

        //METÓDOS PARA CRIAR, ATUALIZAR E EXCLUIR
        [HttpPost]
        public IActionResult CriarUsuario(CriarUsuarioModel usuarioModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = new UsuarioModel()
                    {
                        Id = usuarioModel.Id,
                        Nome = usuarioModel.Nome,
                        Sexo = usuarioModel.Sexo,
                        Perfil = usuarioModel.Perfil,
                        Login = usuarioModel.Login,
                        Email = usuarioModel.Email,
                        Senha = usuarioModel.Senha,
                    };
                    _usuarioRepositorio.Criar(usuario);
                    TempData["MensagemSucesso"] = "Usuário criado com sucesso !";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Preencha todos os campos !";
                    return View("Criar", usuarioModel);
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível criar o seu usuário. Detalhe do erro: {erro.Message}";
                return View("Index");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(EditarUsuarioModel usuarioSemSenha)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenha.Id,
                        Nome = usuarioSemSenha.Nome,
                        Sexo = usuarioSemSenha.Sexo,
                        Perfil = usuarioSemSenha.Perfil,
                        Login = usuarioSemSenha.Login,
                        Email = usuarioSemSenha.Email,
                    };
                    _usuarioRepositorio.Editar(usuario);
                    TempData["MensagemSucesso"] = "Usuário atualizado com sucesso !";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MensagemErro"] = "Preencha todos os campos !";
                    return View("Editar");
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível atualizar o seu usuário. Detalhe do erro: {erro.Message}";
                return View("Index");
            }
        }
        public IActionResult Excluir(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            if (usuario == null)
            {
                TempData["MensagemErro"] = "Não foi possível excluir o seu Usuário.";
                return RedirectToAction("Index");
            }
            _usuarioRepositorio.Excluir(usuario);
            TempData["MensagemSucesso"] = "Usuário excluído com sucesso !";
            return RedirectToAction("Index");
        }
    }
}
