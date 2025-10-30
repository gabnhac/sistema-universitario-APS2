using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Universitario.Dominio.Model;

public class Disciplina
{
    public int Id { get; set; }
    public string Nome {  get; set; }
    public List<Curso> Cursos { get; set; } = new List<Curso>();
}

