using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace web_renderizacao_server_sidee.Helpers
{
    public class LogadoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(string.IsNullOrEmpty(context.HttpContext.Request.Cookies["adm_desafio_21dias_csharp_api"]))
            {
                context.HttpContext.Response.Redirect("/login");
                return;
            }

            if(context.Controller != null)
            {
                string usuarioLogado = context.HttpContext.Request.Cookies["adm_desafio_21dias_csharp_api_nome"];

                ((Controller)context.Controller).TempData["usuarioLogado"] = usuarioLogado;
            }

            base.OnActionExecuting(context);
        }
    }
}