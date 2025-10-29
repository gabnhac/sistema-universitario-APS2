using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.Universitario.Dominio.Model;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Universitario.Web.Models.TurmaViewModel
{
    public class TurmaCreateViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(150, ErrorMessage = "O nome não pode ser maior que 150 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Selecione um curso")]
        public int? CursoId { get; set; }

        public IEnumerable<SelectListItem> CursosDisponiveis { get; set; } = new List<SelectListItem>();
    }
}
