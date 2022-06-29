using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_renderizacao_server_sidee.Servico;

namespace web_renderizacao_server_sidee.Controllers
{
    public class PaisController : Controller
    {
        public async Task<IActionResult> Index([FromQuery]int pagina = 1)
        {
            var listaDePais = await PaiServico.ObterTodos();
            return View(listaDePais);
        }
    }
}