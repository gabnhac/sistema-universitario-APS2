using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.Universitario.Web.Models.CursoViewModel;
using Sistema.Universitario.Web.Models.DisciplinaViewModel;
using Sistema.Universitario.Web.Models.TurmaViewModel;

namespace Sistema.Universitario.Web.Services.Interfaces
{
    public interface IDisciplinaService
    {
        public Task<IEnumerable<DisciplinaViewModel>> GetAllAsync();
        public Task<DisciplinaViewModel> GetByIdAsync(int id);
        public Task SaveAsync(DisciplinaCreateViewModel disciplinaCreateViewModel);
        public Task DeleteAsync(int id);
        public Task<DisciplinaCreateViewModel> PrepararCreateViewModelAsync();
        public Task<IEnumerable<SelectListItem>> ObterCursosParaDropdownAsync();
    }
}
