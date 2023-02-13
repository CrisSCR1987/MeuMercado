using MeuMercado.Filters;
using MeuMercado.Helper.Interfaces;
using MeuMercado.Models;
using MeuMercado.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeuMercado.Controllers
{
    [ParaUsuarioLogadoAdminOuVendedor]
    public class UsuarioVendedor : Controller
    {
        public readonly IProdutoRepositorio _produtoRepositorio;
        public readonly ISessao _sessao;

        public UsuarioVendedor(IProdutoRepositorio produtoRepositorio, ISessao sessao)
        {
            _produtoRepositorio = produtoRepositorio;
            _sessao = sessao;
        }

        [HttpGet]
        public IActionResult MeusProdutos()
        {
            UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();
            List<ProdutoModel> produtos = _produtoRepositorio.BuscarTodosDoUsuario(usuario.Id);
            return View(produtos);
        }
        public IActionResult Sair()
        {
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            ProdutoModel produto = _produtoRepositorio.BuscarPorId(id);
            if (produto == null)
            {
                TempData["MensagemErro"] = "Não foi possível editar o produto.";
                return RedirectToAction("MeusProdutos");
            }
            CriarProdutoModel produtoCriar = new CriarProdutoModel()
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Categoria = produto.Categoria,
                Valor = produto.Valor,
                Quantidade = produto.Quantidade,
            };
            return View(produtoCriar);
        }

        [HttpGet]
        public IActionResult ExcluirConfirmacao(int id)
        {
            ProdutoModel produto = _produtoRepositorio.BuscarPorId(id);
            if (produto == null)
            {
                TempData["MensagemErro"] = "Não foi possível excluir o produto.";
                return RedirectToAction("MeusProdutos");
            }
            return View(produto);
        }

        //METÓDOS PARA CRIAR, ATUALIZAR E EXCLUIR
        [HttpPost]
        public IActionResult CriarProduto(CriarProdutoModel criarProduto)
        {
            try
            {
                UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();
                if (ModelState.IsValid)
                {
                    ProdutoModel produto = new ProdutoModel()
                    {
                        Id = criarProduto.Id,
                        Nome = criarProduto.Nome,
                        Categoria = criarProduto.Categoria,
                        Quantidade = criarProduto.Quantidade,
                        Valor = criarProduto.Valor,
                        UsuarioId = usuario.Id,
                    };
                    _produtoRepositorio.Criar(produto);
                    TempData["MensagemSucesso"] = "Produto cadastrado com sucesso !";
                    return RedirectToAction("MeusProdutos");
                }
                else
                {
                    return View(criarProduto);
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível cadastrar seu produto. Detalhe do erro: {erro.Message}";
                return RedirectToAction("MeusProdutos");
            }
        }

        [HttpPost]
        public IActionResult EditarProduto(CriarProdutoModel editarProduto)
        {
            try
            {
                UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();
                if (ModelState.IsValid)
                {
                    ProdutoModel produto = new ProdutoModel()
                    {
                        Id = editarProduto.Id,
                        Nome = editarProduto.Nome,
                        Categoria = editarProduto.Categoria,
                        Valor = editarProduto.Valor,
                        Quantidade = editarProduto.Quantidade,
                        UsuarioId = usuario.Id,
                    };
                    _produtoRepositorio.Editar(produto);
                    TempData["MensagemSucesso"] = "Produto atualizado com sucesso !";
                    return RedirectToAction("MeusProdutos");
                }
                else
                {
                    return View("Editar", editarProduto);
                };
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível editar seu produto. Detalhe do erro: {erro.Message}";
                return RedirectToAction("MeusProdutos");
            }
        }

        public IActionResult Excluir(int id)
        {
            try
            {
                ProdutoModel produto = _produtoRepositorio.BuscarPorId(id);
                _produtoRepositorio.Excluir(produto.Id);
                TempData["MensagemSucesso"] = "Produto excluído com sucesso !";
                return RedirectToAction("MeusProdutos");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível excluir seu produto. Detalhe do erro: {erro.Message}";
                return RedirectToAction("MeusProdutos");
            }
        }
    }
}
