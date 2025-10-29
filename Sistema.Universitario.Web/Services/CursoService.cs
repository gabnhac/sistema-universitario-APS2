using Microsoft.EntityFrameworkCore;
using Sistema.Universitario.Dominio.Model;
using Sistema.Universitario.Infra.Context;
using Sistema.Universitario.Web.Models.CursoViewModel;
using Sistema.Universitario.Web.Services.Interfaces;
using System.Linq;

namespace Sistema.Universitario.Web.Services
{
    public class CursoService : ICursoService
    {
        private readonly SUDbContext _context;
        public CursoService(SUDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<CursoViewModel>> GetAllAsync()
        {
            return await _context.Cursos
            .AsNoTracking()
            .Select(curso => new CursoViewModel
            {
                Id = curso.Id,
                Nome = curso.Nome,
                Turmas = curso.Turmas.Select(t => t.Nome).ToList(),
                Disciplinas = curso.Disciplinas.Select(d => d.Nome).ToList()
            })
            .ToListAsync();
        }

        public async Task<CursoViewModel> GetByIdAsync(int id)
        {
            var cursoViewModel = await _context.Cursos
                .AsNoTracking()
                .Where(curso => curso.Id == id)
                .Select(curso => new CursoViewModel
                {
                    Id = curso.Id,
                    Nome = curso.Nome,
                    Turmas = curso.Turmas.Select(t => t.Nome).ToList(),
                    Disciplinas = curso.Disciplinas.Select(d => d.Nome).ToList()
                })
                .FirstOrDefaultAsync();

            if (cursoViewModel == null)
            {
                throw new Exception("Não foi encontrado curso com esse id");
            }

            return cursoViewModel;
        }

        public async Task SaveAsync(CursoCreateViewModel cursoCreateViewModel)
        {
            var novoCurso = new Curso
            {
                Nome = cursoCreateViewModel.Nome
            };
            _context.Cursos.Add(novoCurso);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cursoParaDeletar = await _context.Cursos
                                                 .Include(c => c.Turmas)
                                                 .Include(c => c.Disciplinas)
                                                 .FirstOrDefaultAsync(c => c.Id == id);

            if (cursoParaDeletar == null)
            {
                return true;
            }

            if (cursoParaDeletar.Turmas.Any() || cursoParaDeletar.Disciplinas.Any())
            {
                return false;
            }

            _context.Cursos.Remove(cursoParaDeletar);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}