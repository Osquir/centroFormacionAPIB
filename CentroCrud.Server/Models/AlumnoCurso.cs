using System;
using System.Collections.Generic;

namespace CentroCrud.Server.Models;

public partial class AlumnoCurso
{
    public int IdAlumnoCurso { get; set; }

    public int AlumnoId { get; set; }

    public int CursoId { get; set; }
    public int Curso1 { get; set; }

    public int Curso2 { get; set; }

    public int Curso3 { get; set; }

    public int Curso4 { get; set; }
    public int Curso5 { get; set; }

    public virtual Alumno Alumno { get; set; } = null!;

    public virtual Curso Curso { get; set; } = null!;
  
}
