using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema.Universitario.Dominio.Model;
using Sistema.Universitario.Infra.Context;
using Sistema.Universitario.Web.Models.DisciplinaViewModel;
using Sistema.Universitario.Web.Models.TurmaViewModel;
using Sistema.Universitario.Web.Services.Interfaces;

namespace Sistema.Universitario.Web.Services
{
    public class DisciplinaService : IDisciplinaService
    {
        private readonly SUDbContext _context;
        public DisciplinaService(SUDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<DisciplinaViewModel>> GetAllAsync()
        {
            return await _context.Disciplinas
                .AsNoTracking()
                .Select(d => new DisciplinaViewModel
                {
                    Id = d.Id,
                    Nome = d.Nome,
                    NomeDosCursos = d.Cursos.Select(c => c.Nome).ToList()
                })
                .ToListAsync();
        }

        public async Task<DisciplinaViewModel> GetByIdAsync(int id)
        {
            var disciplinaViewModel = await _context.Disciplinas
                .AsNoTracking()
                .Where(d => d.Id == id)
                .Select(d => new DisciplinaViewModel
                {
                    Id = d.Id,
                    Nome = d.Nome,
                    NomeDosCursos = d.Cursos.Select(c => c.Nome).ToList()
                })
                .FirstOrDefaultAsync();

            if (disciplinaViewModel == null)
            {
                throw new Exception("Não foi encontrada disciplina com esse id");
            }

            return disciplinaViewModel;
        }

        public async Task SaveAsync(DisciplinaCreateViewModel disciplinaCreateViewModel)
        {
            var novaDisciplina = new Disciplina
            {
                Nome = disciplinaCreateViewModel.Nome
            };

            if (disciplinaCreateViewModel.CursoIds != null && disciplinaCreateViewModel.CursoIds.Any())
            {
                var cursos = await _context.Cursos
                    .Where(c => disciplinaCreateViewModel.CursoIds.Contains(c.Id))
                    .ToListAsync();

                foreach (var curso in cursos)
                {
                    curso.Disciplinas.Add(novaDisciplina);
                }
                novaDisciplina.Cursos = cursos;
            }

            _context.Disciplinas.Add(novaDisciplina);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var disciplinaParaDeletar = await _context.Disciplinas.FindAsync(id);
            if (disciplinaParaDeletar != null)
            {
                _context.Disciplinas.Remove(disciplinaParaDeletar);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<DisciplinaCreateViewModel> PrepararCreateViewModelAsync()
        {
            var viewModel = new DisciplinaCreateViewModel
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