using Sistema.Universitario.Dominio.Model;

namespace Sistema.Universitario.Web.Models.TurmaViewModel
{
    public class TurmaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<string> NomeDosAlunos { get; set; } = new List<string>();
        public string NomeDoCurso { get; set; }
    }
}
