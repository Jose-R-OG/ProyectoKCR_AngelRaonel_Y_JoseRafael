using System.ComponentModel.DataAnnotations;

namespace KCR.Models;

public class Clientes
{
    [Key]
    public int IdCliente { get; set; }
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s\.-]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
    public string Nombres { get; set; }

    [StringLength(11, MinimumLength = 11, ErrorMessage = "La Cédula debe tener 11 dígitos.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Solo se permiten 11 dígitos numéricos.")]
    public string? Cedula { get; set; }

    [RegularExpression(@"^\d{10,}$", ErrorMessage = "El teléfono solo debe contener números.")]
    public string? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
    public string? Correo { get; set; }
    public DateTime Fecha { get; set; }

    public ICollection<Turnos> Turnos { get; set; } = new List<Turnos>();
    public ICollection<PreFacturas> PreFacturas { get; set; } = new List<PreFacturas>();
}