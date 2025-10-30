using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Universitario.Dominio.Model;

public class Turma
{
    public int Id { get; set; }
    public string Nome {  get; set; }
    public List<Aluno> Alunos { get; set; } = new List<Aluno>();
    public int CursoId { get; set; }
    public Curso Curso { get; set; }

}

