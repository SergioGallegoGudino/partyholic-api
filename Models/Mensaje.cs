using System;
using System.Collections.Generic;

namespace partyholic_api.Models
{
    public partial class Mensaje
    {
        public int IdMensaje { get; set; }
        public string? Username { get; set; }
        public string? Contenido { get; set; }
        public int? CodGrupo { get; set; }

        public virtual Grupo? CodGrupoNavigation { get; set; }
        public virtual Usuario? UsernameNavigation { get; set; }
    }
}
