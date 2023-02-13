using MeuMercado.Filters;
using MeuMercado.Helper.Interfaces;
using MeuMercado.Models;
using MeuMercado.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeuMercado.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly INotaFiscalRepositorio _notaFiscalRepositorio;
        private readonly ISessao _sessao;

        public ProdutoController(IProdutoRepositorio produtoRepositorio, ISessao sessao, INotaFiscalRepositorio notaFiscalRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _sessao = sessao;
            _notaFiscalRepositorio = notaFiscalRepositorio;
        }
        public IActionResult Index()
        { 
            List<ProdutoModel> produtos = _produtoRepositorio.BuscarTodos();
            return View(produtos);
        }
        [HttpGet]
        public IActionResult Comprar(int id)
        {
            ProdutoModel produto = _produtoRepositorio.BuscarPorId(id);
            if (produto == null)
            {
                TempData["MensagemErro"] = "Não foi possível comprar.";
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        public IActionResult ComprarProduto(ProdutoModel produtoComprar)
        {
            UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();
            ProdutoModel produto = _produtoRepositorio.BuscarPorId(produtoComprar.Id);
            produto.Quantidade -= produtoComprar.Quantidade;
            _produtoRepositorio.Editar(produto);
            NotaFiscalModel notaFiscal = new NotaFiscalModel()
            {
                Id = 0,
                Valor = produtoComprar.Quantidade * produto.Valor,
                ProdutoId = produto.Id,
                UsuarioId = usuario.Id,
            };
            _notaFiscalRepositorio.Criar(notaFiscal);
            TempData["MensagemSucesso"] = "Produto comprado com sucesso !";
            return RedirectToAction("Index", "NotaFiscal");
        }
    }
}
