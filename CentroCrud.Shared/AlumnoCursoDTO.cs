using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CentroCrud.Shared
{
  public class AlumnoCursoDTO
  {
    public int IdAlumnoCurso { get; set; }
    [Required(ErrorMessage = "El AlumnoId es requerido")]
    public int AlumnoId { get; set; }
    [Required(ErrorMessage = "El CursoId es requerido")]
    public int CursoId { get; set; }
    public int Curso1 { get; set; }
    public int Curso2 { get; set; }
    public int Curso3 { get; set; }
    public int Curso4 { get; set; }
    public int Curso5 { get; set; }
    public AlumnoDTO? Alumnos { get; set; }
    public CursoDTO? Cursos { get; set; }

  }
}
