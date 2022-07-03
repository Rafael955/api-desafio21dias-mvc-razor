using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_renderizacao_server_sidee.Models;
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

        public async Task<IActionResult> Create()
        {   
            ViewBag.ListaDeAlunos = await ObterListaAlunos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Nome, AlunoId")] Pai pai)
        {
            if(!ModelState.IsValid) 
                return View(pai);

            var _pai = await PaiServico.Salvar(pai);

            return RedirectToAction(nameof(Details), new { id = _pai.Id });
        }

        public async Task<IActionResult> Edit(string id)
        {   
            var pai = await PaiServico.BuscaPorId(id);

            if(pai == null) return NotFound();

            ViewBag.ListaDeAlunos = await ObterListaAlunos();

            return View(pai);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id, Nome, AlunoId")] Pai pai)
        {
            if (id != pai.Id)
            {
                return NotFound();
            }

           if(!ModelState.IsValid) 
                return View(pai);

            var _pai = await PaiServico.Salvar(pai);

            return RedirectToAction(nameof(Details), new { id = _pai.Id });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var pai =  await PaiServico.BuscaPorId(id);

            if (pai == null)
            {
                return NotFound();
            }

            return View(pai);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var exists = await PaiExists(id);
            
            if(!exists) return NotFound();

            await PaiServico.ExcluirPorId(id);
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<IList<Aluno>> ObterListaAlunos()
        {
            return await AlunoServico.ObterTodos();
        }

         private async Task<bool> PaiExists(string id)
        {
            return await PaiServico.BuscaPorId(id) != null ? true : false;
        }
    }
}