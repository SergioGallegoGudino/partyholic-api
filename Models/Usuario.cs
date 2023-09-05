using System;
using System.Collections.Generic;

namespace partyholic_api.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Mensajes = new HashSet<Mensaje>();
            UsuariosEventos = new HashSet<UsuariosEvento>();
            UsuariosGrupos = new HashSet<UsuariosGrupo>();
        }

        public string Username { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? JuegoFavorito { get; set; }
        public string? Privacidad { get; set; }
        public string? FotoPerfil { get; set; }
        public string? Descripcion { get; set; }
        public string? Email { get; set; }
        public string? Passwd { get; set; }
        public string? RolApp { get; set; }

        public virtual ICollection<Mensaje> Mensajes { get; set; }
        public virtual ICollection<UsuariosEvento> UsuariosEventos { get; set; }
        public virtual ICollection<UsuariosGrupo> UsuariosGrupos { get; set; }
    }
}
