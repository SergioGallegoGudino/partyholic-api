using System;
using System.Collections.Generic;

namespace partyholic_api.Models
{
    public partial class GruposLogro
    {
        public int Id { get; set; }
        public int? CodGrupo { get; set; }
        public int? CodLogro { get; set; }
        public bool? Alcanzado { get; set; }
        public int? Actual { get; set; }

        public virtual Grupo? CodGrupoNavigation { get; set; }
        public virtual Logro? CodLogroNavigation { get; set; }
    }
}
