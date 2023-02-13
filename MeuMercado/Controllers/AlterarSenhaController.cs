using MeuMercado.Filters;
using MeuMercado.Helper.Interfaces;
using MeuMercado.Models;
using MeuMercado.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeuMercado.Controllers
{
    [PaginaParaUsuarioLogado]
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AlterarSenhaDoUsuario(AlterarSenhaModel alterarSenha)
        {
            try
            {
                UsuarioModel usuario = _sessao.BuscarSessaoDoUsuario();
                if (usuario.SenhaValida(alterarSenha.SenhaAtual) == true)
                {
                    if (alterarSenha.NovaSenha == alterarSenha.ConfirmarNovaSenha)
                    {
                        usuario.Senha = alterarSenha.NovaSenha;
                        _usuarioRepositorio.AlterarSenha(usuario);
                        TempData["MensagemSucesso"] = "Senha alterada com sucesso !";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Os campos 'Nova Senha' e 'Confirmar Nova Senha' não são iguais.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["MensagemErro"] = "Senha atual informada está incorreta.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível alterar sua senha. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
