using System;
using System.Collections.Generic;

namespace CentroCrud.Server.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<RecuperacionPassword> RecuperacionPasswords { get; } = new List<RecuperacionPassword>();

    public virtual ICollection<UsuarioRol> UsuarioRols { get; } = new List<UsuarioRol>();
}
