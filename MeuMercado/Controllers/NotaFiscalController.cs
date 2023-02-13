using MeuMercado.Filters;
using MeuMercado.Helper.Interfaces;
using MeuMercado.Models;
using MeuMercado.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeuMercado.Controllers
{
    [PaginaParaUsuarioLogado]
    public class NotaFiscalController : Controller
    {
        private readonly ISessao _sessao;
        private readonly INotaFiscalRepositorio _notaFiscalRepositorio;

        public NotaFiscalController(ISessao sessao, INotaFiscalRepositorio notaFiscalRepositorio)
        {
            _sessao = sessao;
            _notaFiscalRepositorio = notaFiscalRepositorio;
        }

        public IActionResult Index()
        {
            UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();
            List<NotaFiscalModel> notasFiscais = _notaFiscalRepositorio.BuscarTodosDoUsuario(usuario.Id);
            return View(notasFiscais);
        }
    }
}
