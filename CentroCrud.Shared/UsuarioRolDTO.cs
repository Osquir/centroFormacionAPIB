using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CentroCrud.Shared
{
  public class UsuarioRolDTO
  {
    public int IdUsuarioRol { get; set; }
    [Required(ErrorMessage = "El UsuarioId es requerido")]
    public int UsuarioId { get; set; }
    [Required(ErrorMessage = "El RolId es requerido")]
    public int RolId { get; set; }
    public UsuarioDTO? Usuarios { get; set; }
    public RolDTO? Rols { get; set; }
  }
}
