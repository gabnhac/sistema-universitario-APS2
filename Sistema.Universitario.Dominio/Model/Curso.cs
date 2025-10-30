using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Universitario.Dominio.Model;

public class Curso
{
    public int Id { get; set; }
    public string Nome {  get; set; }
    public List<Turma> Turmas { get; set; } = new List<Turma>();
    public List<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
}

