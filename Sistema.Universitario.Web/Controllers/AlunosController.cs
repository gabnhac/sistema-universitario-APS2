using Microsoft.AspNetCore.Mvc;
using Sistema.Universitario.Web.Models.AlunoViewModel;
using Sistema.Universitario.Web.Models.CursoViewModel;
using Sistema.Universitario.Web.Services.Interfaces;

namespace Sistema.Universitario.Web.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        public async Task<IActionResult> Index()
        {
            var listaDeAlunos = await _alunoService.GetAllAsync();
            return View(listaDeAlunos);
        }


        public async Task<IActionResult> Create()
        {
            var viewModel = await _alunoService.PrepararCreateViewModelAsync();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlunoCreateViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                await _alunoService.SaveAsync(viewModel);
                TempData["SuccessMessage"] = "Aluno cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            viewModel.TurmasDisponiveis = await _alunoService.ObterTurmasParaDropdownAsync();
            return View(viewModel);
        }


        public async Task<IActionResult> Delete(int id)
        {

            var alunoViewModel = await _alunoService.GetByIdAsync(id);

            if (alunoViewModel == null)
            {

                return NotFound();
            }

            return View(alunoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _alunoService.DeleteAsync(id);

            TempData["SuccessMessage"] = "Aluno excluído com sucesso!";

            return RedirectToAction(nameof(Index));
        }
    }
}
