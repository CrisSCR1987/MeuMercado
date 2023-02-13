using MeuMercado.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MeuMercado.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VendedorEAdmin()
        {
            return View();
        }
    }
}
