using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_renderizacao_server_sidee.Helpers;
using web_renderizacao_server_sidee.Models;
using web_renderizacao_server_sidee.Servico;

namespace web_renderizacao_server_sidee.Controllers
{
    [Logado]
    public class AdministradoresController : Controller
    {
        // GET: Administradores?pagina=1
        public async Task<IActionResult> Index([FromQuery] int pagina = 1)
        {
            return View(await AdministradorServico.ObterTodosPaginado(pagina));
        }

        // GET: Administradores/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var administrador = await AdministradorServico.BuscaPorId(id);

            if (administrador == null)
            {
                return NotFound();
            }

            return View(administrador);
        }

        // GET: Administradores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administradores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Senha")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                var _administrador = await AdministradorServico.Salvar(administrador);
                return RedirectToAction(nameof(Details), new { id = _administrador.Id });
            }
            return View(administrador);
        }

        // GET: Administradores/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var administrador = await AdministradorServico.BuscaPorId(id);

            if (administrador == null)
            {
                return NotFound();
            }

            return View(administrador);
        }

        // POST: Administradores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Senha")] Administrador administrador)
        {
            if (id != administrador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await AdministradorServico.Salvar(administrador);
                return RedirectToAction(nameof(Index));
            }

            return View(administrador);
        }

        // GET: Administradores/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var administrador =  await AdministradorServico.BuscaPorId(id);

            if (administrador == null)
            {
                return NotFound();
            }

            return View(administrador);
        }

        // POST: Administradores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exists = await AdministradorExists(id);
            
            if(!exists) return NotFound();

            await AdministradorServico.ExcluirPorId(id);
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AdministradorExists(int id)
        {
            return await AdministradorServico.BuscaPorId(id) != null ? true : false;
        }
    }
}
