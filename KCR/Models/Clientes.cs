using System.ComponentModel.DataAnnotations;

namespace KCR.Models;

public class Clientes
{
    [Key]
    public int IdCliente { get; set; }
    public string Nombres { get; set; }
    public string? Cedula { get; set; } 
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public DateTime Fecha { get; set; }

    public ICollection<Turnos> Turnos { get; set; } = new List<Turnos>();
    public ICollection<PreFacturas> PreFacturas { get; set; } = new List<PreFacturas>();
}