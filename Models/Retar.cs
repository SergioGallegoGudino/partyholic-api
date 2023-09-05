using System;
using System.Collections.Generic;

namespace partyholic_api.Models
{
    public partial class Retar
    {
        public int Id { get; set; }
        public int? GrupoRetador { get; set; }
        public int? GrupoRetado { get; set; }
        public string? Mensaje { get; set; }
        public bool? Aceptar { get; set; }

        public virtual Grupo? GrupoRetadoNavigation { get; set; }
        public virtual Grupo? GrupoRetadorNavigation { get; set; }
    }
}
