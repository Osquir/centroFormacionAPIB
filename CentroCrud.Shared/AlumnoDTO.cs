using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CentroCrud.Shared
{
  public class AlumnoDTO
  {
    public int IdAlumno { get; set; }
    [Required(ErrorMessage ="El NIF es requerido")]
    public string Nif { get; set; } = null!;
    [Required(ErrorMessage = "El Nombre es requerido")]
    public string PrimerNombre { get; set; } = null!;
    public string? SegundoNombre { get; set; }
    [Required(ErrorMessage = "El Apellido es requerido")]
    public string PrimerApellido { get; set; } = null!;
    public string? SegundoApellido { get; set; }
  }
}
