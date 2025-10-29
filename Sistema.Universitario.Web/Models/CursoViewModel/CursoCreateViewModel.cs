using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Universitario.Web.Models.CursoViewModel
{
    public class CursoCreateViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(150, ErrorMessage = "O nome não pode ser maior que 150 caracteres")]
        public string Nome { get; set; }

    }
}
