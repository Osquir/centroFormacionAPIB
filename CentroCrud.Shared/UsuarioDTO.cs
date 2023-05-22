using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CentroCrud.Shared
{
  public class UsuarioDTO
  {
    public int IdUsuario { get; set; }
    [Required(ErrorMessage = "El Login es requerido")]
    public string Login { get; set; } = null!;
    [Required(ErrorMessage = "El Password es requerido")]
    public string Password { get; set; } = null!;
    [Required(ErrorMessage = "El Nombre es requerido")]
    public string Nombre { get; set; } = null!;
    [Required(ErrorMessage = "El Email es requerido")]
    public string Email { get; set; } = null!;
  }
}
