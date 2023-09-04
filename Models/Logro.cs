﻿using System;
using System.Collections.Generic;

namespace partyholic_api.Models;

public partial class Logro
{
    public int CodLogro { get; set; }

    public string? Nombre { get; set; }

    public int? Objetivo { get; set; }

    public virtual ICollection<GruposLogro> GruposLogros { get; set; } = new List<GruposLogro>();
}
