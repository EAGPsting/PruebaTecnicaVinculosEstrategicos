namespace PortalVisitantes.Models
{
    public class MenuItem
    {
        public decimal Id { get; set; }

        public string? Nombre { get; set; }

        public string? Url { get; set; }

        public decimal? PadreId { get; set; }

        public virtual ICollection<MenuItem> InversePadre { get; set; } = new List<MenuItem>();

        public virtual MenuItem? Padre { get; set; }
    }
}
