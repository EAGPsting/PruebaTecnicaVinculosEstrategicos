using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PortalVisitantes.Models
{
    public class Visitante
    {
        [Required(ErrorMessage = "El DUI es obligatorio")]
        public string Dui { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El Email es obligatorio")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression(@"^[\d+-]+$", ErrorMessage = "Solo se permiten números, + y - en el teléfono")]
        public string Telefono { get; set; }

        [Display(Name = "Generación")]
        public string Generacion { get; set; }
    }
}
