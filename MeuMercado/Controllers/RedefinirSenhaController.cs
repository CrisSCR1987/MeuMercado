using MeuMercado.Filters;
using MeuMercado.Models;
using MeuMercado.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeuMercado.Controllers
{
    [PaginaParaUsuarioDeslogado]
    public class RedefinirSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public RedefinirSenhaController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult NovaSenha(UsuarioModel usuario)
        {
            return View(usuario);
        }
        [HttpPost]
        public IActionResult Redefinir(RedefinirSenhaModel redefinirSenha)
        {
            try
            {
                UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenha.Email, redefinirSenha.Login);
                if (usuario == null)
                {
                    TempData["MensagemErro"] = "Login e/ou Email informado incoreto.";
                    return RedirectToAction("Index");
                }
                string novaSenha = usuario.GerarNovaSenha();
                _usuarioRepositorio.RedefinirSenha(usuario);
                UsuarioModel usuarioNovaSenha = new UsuarioModel()
                {
                    Senha = novaSenha,
                };
                return RedirectToAction("NovaSenha", usuarioNovaSenha);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível redefinir sua senha. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
