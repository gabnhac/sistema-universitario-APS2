namespace Sistema.Universitario.Dominio.Model;

public class Aluno
{
    public int Id { get; set; }
    public int Matricula {  get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public int TurmaId { get; set; }
    public Turma Turma { get; set; }

}
