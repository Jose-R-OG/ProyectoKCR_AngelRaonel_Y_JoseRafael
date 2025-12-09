using KCR.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KCR.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Empleados> empleados { get; set; }
    public DbSet<Clientes> clientes { get; set; }
    public DbSet<Materiales> materiales { get; set; }
    public DbSet<PreFacturas> preFacturas { get; set; }
    public DbSet<Turnos> turnos { get; set; }
    public DbSet<PreFacturaDetalles> preFacturaDetalles { get; set; }
    public DbSet<Servicios> servicios { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Servicios>(entity =>
        {
            entity.HasData(
                new Servicios { IdServicio = 18, Nombre = "SERVICIO EXPRESS", Precio = 0.00 },
                new Servicios { IdServicio = 19, Nombre = "DISEÑO Y EDICIÓN", Precio = 0.00 }
            );
        });


        builder.Entity<PreFacturaDetalles>()
            .HasOne(pd => pd.Materiales)
            .WithMany(m => m.PreFacturaDetalles)
            .HasForeignKey(pd => pd.IdMaterial)
            .IsRequired(false);


        builder.Entity<PreFacturaDetalles>()
            .HasOne(pd => pd.Servicios)
            .WithMany(s => s.PreFacturaDetalles)
            .HasForeignKey(pd => pd.IdServicio)
            .IsRequired(false);
    } 
}