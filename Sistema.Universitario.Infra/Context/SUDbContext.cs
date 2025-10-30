using Microsoft.EntityFrameworkCore;
using Sistema.Universitario.Dominio.Model;
using Sistema.Universitario.Infra.Configurations;

namespace Sistema.Universitario.Infra.Context;

public class SUDbContext : DbContext
{
    public SUDbContext(DbContextOptions<SUDbContext> options)
    : base(options)
    { }

    public DbSet<Disciplina> Disciplinas { get; set; }
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Turma> Turmas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AlunoConfiguration());
        modelBuilder.ApplyConfiguration(new CursoConfiguration());
        modelBuilder.ApplyConfiguration(new DisciplinaConfiguration());
        modelBuilder.ApplyConfiguration(new TurmaConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}  