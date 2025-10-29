using Sistema.Universitario.Web.Models.CursoViewModel;
using Sistema.Universitario.Web.Models.TurmaViewModel;

namespace Sistema.Universitario.Web.Services.Interfaces
{
    public interface ICursoService
    {
        public Task<IEnumerable<CursoViewModel>> GetAllAsync();
        public Task<CursoViewModel> GetByIdAsync(int id);
        public Task SaveAsync(CursoCreateViewModel cursoCreateViewModel);
        public Task<bool> DeleteAsync(int id);
    }
}
