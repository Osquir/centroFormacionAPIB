using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CentroCrud.Shared
{
  public class CursoDTO
  {
    public int IdCurso { get; set; }
    [Required(ErrorMessage = "El Codigo es requerido")]
    public string Codigo { get; set; } = null!;
    [Required(ErrorMessage = "El nombre es requerido")]
    public string Nombre { get; set; } = null!;
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El valor debe estar entre 1 y 100")]
    public int Creditos { get; set; }
    public string? Descripcion { get; set; }
    public string? Temario { get; set; }
  }
}
