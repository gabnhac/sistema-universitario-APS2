using Microsoft.AspNetCore.Mvc;
using Sistema.Universitario.Web.Models.DisciplinaViewModel;
using Sistema.Universitario.Web.Models.TurmaViewModel;
using Sistema.Universitario.Web.Services.Interfaces;

namespace Sistema.Universitario.Web.Controllers
{
    public class DisciplinasController : Controller
    {
        private readonly IDisciplinaService _disciplinaService;

        public DisciplinasController(IDisciplinaService disciplinaService)
        {
            _disciplinaService = disciplinaService;
        }

        public async Task<IActionResult> Index()
        {
            var listaDeDisciplinas = await _disciplinaService.GetAllAsync();
            return View(listaDeDisciplinas);
        }


        public async Task<IActionResult> Create()
        {
            var viewModel = await _disciplinaService.PrepararCreateViewModelAsync();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DisciplinaCreateViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                await _disciplinaService.SaveAsync(viewModel);
                TempData["SuccessMessage"] = "Disciplina cadastrada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            viewModel.CursosDisponiveis = await _disciplinaService.ObterCursosParaDropdownAsync();
            return View(viewModel);
        }


        public async Task<IActionResult> Delete(int id)
        {

            var disciplinaViewModel = await _disciplinaService.GetByIdAsync(id);

            if (disciplinaViewModel == null)
            {

                return NotFound();
            }

            return View(disciplinaViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _disciplinaService.DeleteAsync(id);

            TempData["SuccessMessage"] = "Disciplina excluída com sucesso!";

            return RedirectToAction(nameof(Index));
        }
    }
}
