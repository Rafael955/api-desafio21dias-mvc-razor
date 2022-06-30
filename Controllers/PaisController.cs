using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_renderizacao_server_sidee.Servico;

namespace web_renderizacao_server_sidee.Controllers
{
    public class PaisController : Controller
    {
        private const int QUANTIDADE_POR_PAGINA = 3;

        public async Task<IActionResult> Index([FromQuery]int pagina = 1)
        {
            var listaDePais = await PaiServico.ObterTodos();
            return View(listaDePais);
        }

        public async Task<IActionResult> Details(string id)
        {
            var pai = await PaiServico.BuscaPorId(id);
            
            if (pai == null)
                return NotFound();

            return View(pai);
        }
    }
}