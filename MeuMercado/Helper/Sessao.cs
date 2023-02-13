using MeuMercado.Helper.Interfaces;
using MeuMercado.Models;
using Newtonsoft.Json;

namespace MeuMercado.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;
        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public UsuarioModel BuscarSessaoDoUsuario()
        {
            string valor = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (valor == null)
            {
                return null;
            }
            UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(valor);
            return usuario;
        }

        public void CriarSessaoDoUsuario(UsuarioModel usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);
        }

        public void RemoverSessaoDoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }
    }
}
