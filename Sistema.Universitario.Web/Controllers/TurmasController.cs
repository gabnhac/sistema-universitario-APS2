using Microsoft.AspNetCore.Mvc;
using Sistema.Universitario.Web.Models.TurmaViewModel;
using Sistema.Universitario.Web.Services.Interfaces;

namespace Sistema.Universitario.Web.Controllers
{
    public class TurmasController : Controller
    {
        private readonly ITurmaService _turmaService;

        public TurmasController(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        public async Task<IActionResult> Index()
        {
            var listaDeTurmas = await _turmaService.GetAllAsync();
            return View(listaDeTurmas);
        }


        public async Task<IActionResult> Create()
        {
            var viewModel = await _turmaService.PrepararCreateViewModelAsync();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TurmaCreateViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                await _turmaService.SaveAsync(viewModel);
                TempData["SuccessMessage"] = "Turma cadastrada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            viewModel.CursosDisponiveis = await _turmaService.ObterCursosParaDropdownAsync();
            return View(viewModel);
        }


        public async Task<IActionResult> Delete(int id)
        {

            var turmaViewModel = await _turmaService.GetByIdAsync(id);

            if (turmaViewModel == null)
            {

                return NotFound();
            }

            return View(turmaViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var sucesso = await _turmaService.DeleteAsync(id);

            if (sucesso)
            {
                TempData["SuccessMessage"] = "Turma excluída com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Não é possível excluir esta turma pois ela possui alunos matriculados.";
                return RedirectToAction(nameof(DeleteFail));
            }
        }

        public IActionResult DeleteFail()
        {
            return View();
        }
    }
}
