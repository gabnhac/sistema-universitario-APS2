using Microsoft.AspNetCore.Mvc;
using Sistema.Universitario.Web.Models.CursoViewModel;
using Sistema.Universitario.Web.Services.Interfaces;

namespace Sistema.Universitario.Web.Controllers
{
    public class CursosController : Controller
    {
        private readonly ICursoService _cursoService;

        public CursosController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        public async Task<IActionResult> Index()
        {
            var listaDeCursos = await _cursoService.GetAllAsync();
            return View(listaDeCursos);
        }


        public IActionResult Create()
        {
            return View(new CursoCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CursoCreateViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                await _cursoService.SaveAsync(viewModel);
                TempData["SuccessMessage"] = "Curso cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }


        public async Task<IActionResult> Delete(int id)
        {

            var cursoViewModel = await _cursoService.GetByIdAsync(id);

            if (cursoViewModel == null)
            {

                return NotFound();
            }

            return View(cursoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var sucesso = await _cursoService.DeleteAsync(id);

            if (sucesso)
            {
                TempData["SuccessMessage"] = "Curso excluído com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Não é possível excluir este curso pois ele possui turmas ou disciplinas associadas.";
                return RedirectToAction(nameof(DeleteFail));
            }
        }

        public IActionResult DeleteFail()
        {
            return View();
        }
    }
}
