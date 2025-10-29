using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sistema.Universitario.Web.Models.AlunoViewModel
{
    public class AlunoCreateViewModel
    {
        [Required(ErrorMessage = "A matrícula é obrigatória")]
        public int Matricula { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(150, ErrorMessage = "O nome não pode ser maior que 150 caracteres")]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A turma é obrigatória")]
        public int? TurmaId { get; set; }

        public IEnumerable<SelectListItem> TurmasDisponiveis { get; set; } = new List<SelectListItem>();
    }
}
