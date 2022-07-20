using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_renderizacao_server_sidee.Helpers;
using web_renderizacao_server_sidee.Models;
using web_renderizacao_server_sidee.Servico;

namespace web_renderizacao_server_sidee.Controllers
{
    [Logado]
    public class MateriaisController : Controller
    {

        public async Task<IActionResult> Index([FromQuery] int pagina = 1)
        {
            var materiais = await MaterialServico.ObterTodos(pagina);
            return View(materiais);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(await ObterListaAlunos());
        }

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Id, Nome, AlunoId")] Material material)
        // {
        //     if(!ModelState.IsValid) 
        //         return View(material);

        //     // Salvar material aqui
        // }

        private async Task<IList<Aluno>> ObterListaAlunos()
        {
            return await AlunoServico.ObterTodos();
        }
    }
}