using KCR.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KCR.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Empleados> empleados { get; set; }
        public DbSet<Clientes> clientes { get; set; }
        public DbSet<Materiales> materiales { get; set; }
        public DbSet<PreFacturas> preFacturas { get; set; }
        public DbSet<Turnos> turnos { get; set; }
        public DbSet<PreFacturaDetalles> preFacturaDetalles { get; set; }
        public DbSet<Servicios> servicios { get; set; }

    }
}
