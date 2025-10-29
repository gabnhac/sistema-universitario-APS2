using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema.Universitario.Dominio.Model;
using Sistema.Universitario.Infra.Context;
using Sistema.Universitario.Web.Models.TurmaViewModel;
using Sistema.Universitario.Web.Services.Interfaces;

namespace Sistema.Universitario.Web.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly SUDbContext _context;
        public TurmaService(SUDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<TurmaViewModel>> GetAllAsync()
        {
            return await _context.Turmas
                .AsNoTracking()
                .Select(t => new TurmaViewModel
                {
                    Id = t.Id,
                    Nome = t.Nome,
                    NomeDoCurso = t.Curso != null ? t.Curso.Nome : "Sem Curso",
                    NomeDosAlunos = t.Alunos.Select(a => a.Nome).ToList()
                })
                .ToListAsync();
        }

        public async Task<TurmaViewModel> GetByIdAsync(int id)
        {
            var turmaViewModel = await _context.Turmas
                .AsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new TurmaViewModel
                {
                    Id = t.Id,
                    Nome = t.Nome,
                    NomeDoCurso = t.Curso != null ? t.Curso.Nome : "Sem Curso",
                    NomeDosAlunos = t.Alunos.Select(a => a.Nome).ToList()
                })
                .FirstOrDefaultAsync();

            if (turmaViewModel == null)
            {
                throw new Exception("Não foi encontrada turma com esse id");
            }

            return turmaViewModel;
        }

        public async Task SaveAsync(TurmaCreateViewModel turmaCreateViewModel)
        {
 
            var novaTurma = new Turma
            {
                Nome = turmaCreateViewModel.Nome
            };

            if (turmaCreateViewModel.CursoId != null)
            {
                var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == turmaCreateViewModel.CursoId);
                if (curso == null)
                {
                    throw new Exception("Não foi encontrado curso com esse id");
                }
                curso.Turmas.Add(novaTurma);
                novaTurma.Curso = curso;
                novaTurma.CursoId = curso.Id;
            }

            _context.Turmas.Add(novaTurma);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var turmaParaDeletar = await _context.Turmas
                                                 .Include(t => t.Alunos)
                                                 .FirstOrDefaultAsync(t => t.Id == id);

            if (turmaParaDeletar == null)
            {
                return true;
            }

            if (turmaParaDeletar.Alunos.Any())
            {
                return false;
            }

            _context.Turmas.Remove(turmaParaDeletar);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TurmaCreateViewModel> PrepararCreateViewModelAsync()
        {
            var viewModel = new TurmaCreateViewModel
            {
                CursosDisponiveis = await ObterCursosParaDropdownAsync()
            };
            return viewModel;
        }

        public async Task<IEnumerable<SelectListItem>> ObterCursosParaDropdownAsync()
        {
            return await _context.Cursos
                                 .AsNoTracking()
                                 .OrderBy(c => c.Nome)
                                 .Select(c => new SelectListItem
                                 {
                                     Value = c.Id.ToString(),
                                     Text = c.Nome
                                 })
                                 .ToListAsync();
        }
    }
}