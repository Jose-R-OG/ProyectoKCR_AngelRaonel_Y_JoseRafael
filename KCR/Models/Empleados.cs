using System.ComponentModel.DataAnnotations;

namespace KCR.Models;

public class Empleados
{
    [Key]
    public int IdEmpleado { get; set; }
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string Usuario { get; set; } 
    [Required, MaxLength(255)]
    public string Clave { get; set; } 
    public string? Cedula { get; set; } 
    public string? Cargo { get; set; }

    public ICollection<PreFacturas> PreFacturas { get; set; } = new List<PreFacturas>();
}
