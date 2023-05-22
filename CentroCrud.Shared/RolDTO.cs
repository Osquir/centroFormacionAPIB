﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CentroCrud.Shared
{
  public class RolDTO
  {
    public int IdRol { get; set; }
    [Required(ErrorMessage = "El nombre es requerido")]
    public string Nombre { get; set; } = null!;
  }
}
