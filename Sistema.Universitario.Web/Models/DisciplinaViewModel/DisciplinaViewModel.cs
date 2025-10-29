using Sistema.Universitario.Dominio.Model;

namespace Sistema.Universitario.Web.Models.DisciplinaViewModel
{
    public class DisciplinaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<string> NomeDosCursos { get; set; } = new List<string>();
    }
}
