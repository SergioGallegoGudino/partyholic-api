using System;
using System.Collections.Generic;

namespace partyholic_api.Models
{
    public partial class Grupo
    {
        public Grupo()
        {
            Eventos = new HashSet<Evento>();
            GruposLogros = new HashSet<GruposLogro>();
            Mensajes = new HashSet<Mensaje>();
            RetarGrupoRetadoNavigations = new HashSet<Retar>();
            RetarGrupoRetadorNavigations = new HashSet<Retar>();
            UsuariosGrupos = new HashSet<UsuariosGrupo>();
        }

        public int CodGrupo { get; set; }
        public string? Nombre { get; set; }
        public string? Juego { get; set; }
        public string? Privacidad { get; set; }
        public string? Descripcion { get; set; }
        public string? FotoGrupo { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; }
        public virtual ICollection<GruposLogro> GruposLogros { get; set; }
        public virtual ICollection<Mensaje> Mensajes { get; set; }
        public virtual ICollection<Retar> RetarGrupoRetadoNavigations { get; set; }
        public virtual ICollection<Retar> RetarGrupoRetadorNavigations { get; set; }
        public virtual ICollection<UsuariosGrupo> UsuariosGrupos { get; set; }
    }
}
