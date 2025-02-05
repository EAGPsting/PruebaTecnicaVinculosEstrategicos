using System;
using System.Collections.Generic;

namespace Oracle.DataAccess.Models;

public partial class Visitante
{
    public string Dui { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Email { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public string? Generacion { get; set; }
}
