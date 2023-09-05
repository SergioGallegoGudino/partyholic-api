using System;
using System.Collections.Generic;

namespace partyholic_api.Models
{
    public partial class UsuariosGrupo
    {
        public int Id { get; set; }
        public int? CodGrupo { get; set; }
        public string? Username { get; set; }
        public bool? EsAdmin { get; set; }

        public virtual Grupo? CodGrupoNavigation { get; set; }
        public virtual Usuario? UsernameNavigation { get; set; }
    }
}
