using MeuMercado.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace MeuMercado.Filters
{
    public class PaginaParaUsuarioLogadoEAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessaoDoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");
             if (sessaoDoUsuario == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }
             else
            {
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoDoUsuario);
                 if (usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, {"action", "Index" } });
                }
                 else
                {
                    if (usuario.Perfil == Enums.PerfilEnum.Comum || usuario.Perfil == Enums.PerfilEnum.Vendedor)
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
                    }
                }
            };

            base.OnActionExecuting(context);
        }
    }
}
