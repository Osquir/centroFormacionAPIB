using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CentroCrud.Shared
{
  public  class RecuperacionPasswordDTO
  {
    public int IdRecPass { get; set; }
    [Required(ErrorMessage = "El Usuario es requerido")]
    public int UsuarioId { get; set; }
    public string Token { get; set; } = null!;
  }
}
