using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web_renderizacao_server_sidee.Helpers;
using web_renderizacao_server_sidee.Servico;

namespace web_renderizacao_server_sidee.Controllers
{
    
    public class LoginController : Controller
    { 
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Logar(string email, string senha, string lembrarme)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                ViewBag.Error = "Digite o e-mail e a senha";
                return View(nameof(Index));
            }

            var adm = await AdministradorServico.Logar(email, senha);

            if(adm != null)
            {
                var expirar = DateTimeOffset.UtcNow.AddHours(3);

                if(!string.IsNullOrEmpty(lembrarme))
                    expirar = DateTimeOffset.UtcNow.AddMonths(1);

                HttpContext.Response.Cookies.Append("adm_desafio_21dias_csharp_api", adm.Id.ToString(), new 
                CookieOptions
                {
                    Expires = expirar,
                    HttpOnly = true
                });

                HttpContext.Response.Cookies.Append("adm_desafio_21dias_csharp_api_nome", adm.Nome.ToString(), new 
                CookieOptions
                {
                    Expires = expirar,
                    HttpOnly = true
                });

                Response.Redirect("/");
            }
            else
            {
                ViewBag.Error = "Usuário ou senha inválidas!";
            }

            return View(nameof(Index));
        }

        public async Task<IActionResult> Logoff()
        {
            this.HttpContext.Response.Cookies.Append("adm_desafio_21dias_csharp_api", "", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(-1),
                HttpOnly = true
            });

            this.HttpContext.Response.Cookies.Append("adm_desafio_21dias_csharp_api_nome", "", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(-1),
                HttpOnly = true
            });

            return await Task.Run(() => Redirect("/login"));
        }
    }
}