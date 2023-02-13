using MeuMercado.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MeuMercado.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoDoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (sessaoDoUsuario == null)
            {
                return null;
            }
            else
            {
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoDoUsuario);
                return View(usuario);
            }
        }
    }
}
