using MeuMercado.Filters;
using MeuMercado.Helper.Interfaces;
using MeuMercado.Models;
using MeuMercado.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeuMercado.Controllers
{
    [PaginaParaUsuarioDeslogado]
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);
                     if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha) == true)
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["MensagemErro"] = "Senha informada está incorreta.";
                            return RedirectToAction("Index");
                        }
                    }
                     else
                    {
                        TempData["MensagemErro"] = "Login informado está incorreto.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return View("Index", loginModel);
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível realizar o seu login. Detalhe do erro: {erro.Message}";
                return View("Index");
            }
        }
        [HttpGet]
        public IActionResult ComumOuVendedor()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CriarUsuarioVendedor()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CriarUsuarioComum() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Criar(CriarUsuarioModel criarUsuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = new UsuarioModel()
                    {
                        Id = criarUsuario.Id,
                        Nome = criarUsuario.Nome,
                        Sexo = criarUsuario.Sexo,
                        Perfil = criarUsuario.Perfil,
                        Login = criarUsuario.Login,
                        Email = criarUsuario.Email,
                        Senha = criarUsuario.Senha,
                    };
                    _usuarioRepositorio.Criar(usuario);
                    TempData["MensagemSucesso"] = "Usuário criado com sucesso !";
                    return View("Index");
                }
                else
                {
                    return View("ComumOuVendedor");
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível criar seu usuário. Detalhe do erro: {erro.Message}";
                return View("Index");
            }
        }
    }
}
