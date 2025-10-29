using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema.Universitario.Dominio.Model;
using Sistema.Universitario.Infra.Context;
using Sistema.Universitario.Web.Models.AlunoViewModel;
using Sistema.Universitario.Web.Services.Interfaces;

namespace Sistema.Universitario.Web.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly SUDbContext _context;
        public AlunoService(SUDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<AlunoViewModel>> GetAllAsync()
        {

            return await _context.Alunos
                .AsNoTracking()
                .Select(aluno => new AlunoViewModel
                {
                    Id = aluno.Id,
                    Matricula = aluno.Matricula,
                    Nome = aluno.Nome,
                    Email = aluno.Email,
                    NomeDaTurma = aluno.Turma != null ? aluno.Turma.Nome : "Sem Turma"
                })
                .ToListAsync();
        }

        public async Task<AlunoViewModel> GetByIdAsync(int id)
        {
            var alunoViewModel = await _context.Alunos
                .AsNoTracking()
                .Where(aluno => aluno.Id == id)
                .Select(aluno => new AlunoViewModel
                {
                    Id = aluno.Id,
                    Matricula = aluno.Matricula,
                    Nome = aluno.Nome,
                    Email = aluno.Email,
                    NomeDaTurma = aluno.Turma != null ? aluno.Turma.Nome : "Sem Turma"
                })
                .FirstOrDefaultAsync();

            if (alunoViewModel == null)
            {
                throw new Exception("Não foi encontrado aluno com esse id");
            }

            return alunoViewModel;
        }

        public async Task SaveAsync(AlunoCreateViewModel alunoCreateViewModel)
        {
            var novoAluno = new Aluno
            {
                Nome = alunoCreateViewModel.Nome,
                Matricula = alunoCreateViewModel.Matricula,
                Email = alunoCreateViewModel.Email
            };

            if (alunoCreateViewModel.TurmaId != null)
            {
                var turma = await _context.Turmas.FirstOrDefaultAsync(t => t.Id == alunoCreateViewModel.TurmaId);
                if (turma == null)
                {
                    throw new Exception("Não foi encontrada turma com esse id");
                }
                turma.Alunos.Add(novoAluno);
                novoAluno.Turma = turma;
                novoAluno.TurmaId = turma.Id;
            }

            _context.Alunos.Add(novoAluno);
            await _context.SaveChangesAsync();
        }

        async public Task DeleteAsync(int id)
        {
            var alunoParaDeletar = await _context.Alunos.FindAsync(id);
            if (alunoParaDeletar != null)
            {
                _context.Alunos.Remove(alunoParaDeletar);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AlunoCreateViewModel> PrepararCreateViewModelAsync()
        {
            var viewModel = new AlunoCreateViewModel
            {
                TurmasDisponiveis = await ObterTurmasParaDropdownAsync()
            };
            return viewModel;
        }

        public async Task<IEnumerable<SelectListItem>> ObterTurmasParaDropdownAsync()
        {
            var turmas = await _context.Turmas
                                       .AsNoTracking()
                                       .OrderBy(t => t.Nome)
                                       .Select(t => new { t.Id, t.Nome })
                                       .ToListAsync();

            return turmas.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Nome
            });
        }
    }
}