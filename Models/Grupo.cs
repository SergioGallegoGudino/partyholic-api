using System;
using System.Collections.Generic;

namespace partyholic_api.Models;

public partial class Grupo
{
    public int CodGrupo { get; set; }

    public string? Nombre { get; set; }

    public string? Juego { get; set; }

    public string? Privacidad { get; set; }

    public string? Descripcion { get; set; }

    public string? FotoGrupo { get; set; }

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public virtual ICollection<GruposLogro> GruposLogros { get; set; } = new List<GruposLogro>();

    public virtual ICollection<Mensaje> Mensajes { get; set; } = new List<Mensaje>();

    public virtual ICollection<Retar> RetarGrupoRetadoNavigations { get; set; } = new List<Retar>();

    public virtual ICollection<Retar> RetarGrupoRetadorNavigations { get; set; } = new List<Retar>();

    public virtual ICollection<UsuariosGrupo> UsuariosGrupos { get; set; } = new List<UsuariosGrupo>();
}
