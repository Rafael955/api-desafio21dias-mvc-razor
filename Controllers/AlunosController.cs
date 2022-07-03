using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_renderizacao_server_sidee.Helpers;
using web_renderizacao_server_sidee.Models;
using web_renderizacao_server_sidee.Servico;

namespace web_renderizacao_server_sidee.Controllers
{
    [Logado]
    public class AlunosController : Controller
    {
        // GET: Alunos
        public async Task<IActionResult> Index([FromQuery]int pagina = 1)
        {
            return View(await AlunoServico.ObterTodosPaginado(pagina));
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var aluno = await AlunoServico.BuscaPorId(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Matricula,Notas")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                var _administrador = await AlunoServico.Salvar(aluno);
                return RedirectToAction(nameof(Details), new { id = _administrador.Id });
            }
            return View(aluno);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var aluno = await AlunoServico.BuscaPorId(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Matricula,Notas")] Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await AlunoServico.Salvar(aluno);
                return RedirectToAction(nameof(Index));
            }

            return View(aluno);
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var aluno =  await AlunoServico.BuscaPorId(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exists = await AlunoExists(id);
            
            if(!exists) return NotFound();

            await AlunoServico.ExcluirPorId(id);
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AlunoExists(int id)
        {
            return await AlunoServico.BuscaPorId(id) != null ? true : false;
        }
    }
}
