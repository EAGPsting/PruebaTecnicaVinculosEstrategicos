using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Oracle.DataAccess.Models;

public partial class MenuItem
{
    public decimal Id { get; set; }

    public string? Nombre { get; set; }

    public string? Url { get; set; }

    public decimal? PadreId { get; set; }

    // Ignorar la propiedad InversePadre en la serialización
    [JsonIgnore]
    public virtual ICollection<MenuItem> InversePadre { get; set; } = new List<MenuItem>();

    public virtual MenuItem? Padre { get; set; }
}
