using System;
using System.Collections.Generic;

namespace partyholic_api.Models
{
    public partial class Evento
    {
        public int CodEvento { get; set; }
        public int CodGrupo { get; set; }
        public string? Titulo { get; set; }
        public DateOnly? FechaEvento { get; set; }

        public virtual Grupo CodGrupoNavigation { get; set; } = null!;
    }
}
