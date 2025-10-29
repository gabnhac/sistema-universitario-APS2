using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.Universitario.Web.Models.AlunoViewModel;
using Sistema.Universitario.Web.Models.TurmaViewModel;

namespace Sistema.Universitario.Web.Services.Interfaces
{
    public interface IAlunoService
    {
        public Task<IEnumerable<AlunoViewModel>> GetAllAsync();
        public Task<AlunoViewModel> GetByIdAsync(int id);
        public Task SaveAsync(AlunoCreateViewModel alunoCreateViewModel);
        public Task DeleteAsync(int id);
        public Task<AlunoCreateViewModel> PrepararCreateViewModelAsync();
        public Task<IEnumerable<SelectListItem>> ObterTurmasParaDropdownAsync();
    }
}
