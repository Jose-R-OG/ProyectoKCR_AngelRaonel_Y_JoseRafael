using System.ComponentModel.DataAnnotations;

namespace ProyectoKCR.Models;

public class Servicios
{
    [Key]
    public int IdServicio { get; set; }
    [Required, MaxLength(100)]
    public string Nombre { get; set; }
    public decimal? Precio { get; set; }

    public ICollection<Turnos> Turnos { get; set; } = new List<Turnos>();
}
