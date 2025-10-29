using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.Universitario.Web.Models.AlunoViewModel;
using Sistema.Universitario.Web.Models.CursoViewModel;
using Sistema.Universitario.Web.Models.TurmaViewModel;

namespace Sistema.Universitario.Web.Services.Interfaces
{
    public interface ITurmaService
    {
        public Task<IEnumerable<TurmaViewModel>> GetAllAsync();
        public Task<TurmaViewModel> GetByIdAsync(int id);
        public Task SaveAsync(TurmaCreateViewModel turmaCreateViewModel);
        public Task<bool> DeleteAsync(int id);
        public Task<TurmaCreateViewModel> PrepararCreateViewModelAsync();
        public Task<IEnumerable<SelectListItem>> ObterCursosParaDropdownAsync();
    }
}
