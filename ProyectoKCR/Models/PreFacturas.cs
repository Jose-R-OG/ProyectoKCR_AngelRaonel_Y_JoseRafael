using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoKCR.Models;

public class PreFacturas
{
    [Key]
    public int IdPreFactura { get; set; }
    [MaxLength(255)]
    public string? NombreCliente { get; set; }
    public DateTime Fecha { get; set; }
    [Required, MaxLength(50)]
    public string Estado { get; set; }

    [ForeignKey("Cliente")]
    public int IdCliente { get; set; }
    public Clientes Cliente { get; set; }

    [ForeignKey("Empleado")]
    public int IdEmpleado { get; set; }
    public Empleados Empleado { get; set; }

    public ICollection<DetallePreFactura> Detalles { get; set; } = new List<DetallePreFactura>();
}
