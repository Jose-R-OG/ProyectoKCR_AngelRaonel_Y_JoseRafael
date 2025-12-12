using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCR.Models;

public class Servicios
{
    [Key]
    public int IdServicio { get; set; }
    public string? TipoServicio { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")] 
    public decimal Precio { get; set; }

    public bool Activo { get; set; } = true; 


    [ForeignKey("Materiales")]
    public int? IdMaterial { get; set; }
    public Materiales? Materiales { get; set; }

    [Range(0.5, 1.0, ErrorMessage = "El consumo debe ser 0.5 o 1.0.")]
    [RegularExpression(@"^(0\.5|1(\.0)?)$", ErrorMessage = "El consumo solo puede ser 0.5 o 1.0.")]
    public double? ConsumoPorUnidad { get; set; }

    public ICollection<Turnos> Turnos { get; set; } = new List<Turnos>();
    public ICollection<PreFacturaDetalles> PreFacturaDetalles { get; set; } = new List<PreFacturaDetalles>();
}
