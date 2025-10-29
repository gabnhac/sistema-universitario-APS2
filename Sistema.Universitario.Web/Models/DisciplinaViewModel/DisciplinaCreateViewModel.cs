using Microsoft.AspNetCore.Mvc.Rendering;
using Sistema.Universitario.Dominio.Model;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Universitario.Web.Models.DisciplinaViewModel
{
    public class DisciplinaCreateViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(150, ErrorMessage = "O nome não pode ser maior que 150 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Selecione um curso")]
        public List<int> CursoIds { get; set; }

        public IEnumerable<SelectListItem> CursosDisponiveis { get; set; } = new List<SelectListItem>();
    }
}
