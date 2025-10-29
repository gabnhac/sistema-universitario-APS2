namespace Sistema.Universitario.Web.Models.CursoViewModel
{
    public class CursoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<string> Turmas { get; set; } = new List<string>();
        public List<string> Disciplinas { get; set; } = new List<string>();
    }
}
