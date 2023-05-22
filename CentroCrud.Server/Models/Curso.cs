using System;
using System.Collections.Generic;

namespace CentroCrud.Server.Models;

public partial class Curso
{
    public int IdCurso { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public int Creditos { get; set; }

    public string Descripcion { get; set; }

    public string Temario { get; set; }

    public virtual ICollection<AlumnoCurso> AlumnoCursos { get; } = new List<AlumnoCurso>();
}
