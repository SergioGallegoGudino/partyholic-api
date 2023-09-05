using System;
using System.Collections.Generic;

namespace partyholic_api.Models
{
    public partial class UsuariosEvento
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public int? CodGrupo { get; set; }
        public int? CodEvento { get; set; }
        public bool? Aceptar { get; set; }

        public virtual Usuario? UsernameNavigation { get; set; }
    }
}
