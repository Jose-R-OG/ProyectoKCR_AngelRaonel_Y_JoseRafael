using System.ComponentModel.DataAnnotations;

namespace ProyectoKCR.Models;

public class Empleados
{
    [Key]
    public int IdEmpleado { get; set; }
    [Required, MaxLength(255)]
    public string Nombre { get; set; }
    [Required, MaxLength(50)]
    public string Usuario { get; set; }
    [Required, MaxLength(255)]
    public string Clave { get; set; }
    [MaxLength(20)]
    public string Cedula { get; set; }
    [MaxLength(100)]
    public string Cargo { get; set; }

    public ICollection<PreFacturas> preFacturas { get; set; } = new List<PreFacturas>();
}