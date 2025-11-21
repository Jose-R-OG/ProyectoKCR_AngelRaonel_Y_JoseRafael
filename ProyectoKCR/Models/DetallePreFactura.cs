using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoKCR.Models;

public class DetallePreFactura
{
    [Key]
    public int IdDetalle { get; set; }
    [MaxLength(255)]
    public string? Servicio { get; set; }
    public int Cantidad { get; set; }
    [Required]
    public decimal PrecioUnitarioHistorico { get; set; }

    [ForeignKey("Material")]
    public int IdMaterial { get; set; }
    public Materiales Material { get; set; }

    [ForeignKey("PreFactura")]
    public int IdPreFactura { get; set; }
    public PreFacturas PreFactura { get; set; }
}
