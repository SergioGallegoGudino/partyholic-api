using System;
using System.Collections.Generic;

namespace partyholic_api.Models;

public partial class Usuario
{
    public string Username { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? JuegoFavorito { get; set; }

    public string? Privacidad { get; set; }

    public string? FotoPerfil { get; set; }

    public string? Descripcion { get; set; }

    public string? Email { get; set; }

    public string? Passwd { get; set; }

    public string? RolApp { get; set; }

    public virtual ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();

    public virtual ICollection<UsuariosEvento> UsuariosEventos { get; set; } = new List<UsuariosEvento>();

    public virtual ICollection<UsuariosGrupo> UsuariosGrupos { get; set; } = new List<UsuariosGrupo>();
}
