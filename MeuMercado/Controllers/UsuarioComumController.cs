using MeuMercado.Helper.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeuMercado.Controllers
{
    public class UsuarioComumController : Controller
    {
        private readonly ISessao _sessao;

        public UsuarioComumController(ISessao sessao)
        {
            _sessao= sessao;
        }
        public IActionResult Sair()
        {
                _sessao.RemoverSessaoDoUsuario();
                return RedirectToAction("Index", "Login");
        }
    }
}
