using KCR.Data;
using KCR.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static KCR.Components.EmpleadoPages.PreFacturaEm; 

namespace KCR.Services;

public class PreFacturaService(IDbContextFactory<ApplicationDbContext> DbFactory)
{

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.preFacturas.AnyAsync(p => p.IdPreFactura == id);
    }

    public async Task AfectarExistencia(PreFacturaDetalles[] detalle, TipoOperacion tipoOperacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        foreach (var item in detalle)
        {
            Servicios? servicio = item.Servicios;

            if (servicio == null && item.IdServicio.HasValue)
            {
                servicio = await contexto.servicios
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.IdServicio == item.IdServicio);
            }

            if (servicio == null || servicio.IdMaterial == null || servicio.IdMaterial <= 0)
            {
                continue;
            }

            var material = await contexto.materiales.SingleOrDefaultAsync(m => m.IdMaterial == servicio.IdMaterial);

            if (material != null)
            {
                if (tipoOperacion == TipoOperacion.Suma)
                    material.Existencia += item.Cantidad;
                else
                    material.Existencia -= item.Cantidad;

                contexto.materiales.Update(material);
            }
        }
        await contexto.SaveChangesAsync();
    }


    public async Task<bool> Insertar(PreFacturas preFactura)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.preFacturas.Add(preFactura);
        await AfectarExistencia(preFactura.PreFacturaDetalles.ToArray(), TipoOperacion.Resta);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(PreFacturas preFactura)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var detallesOriginales = await contexto.preFacturaDetalles
            .Where(d => d.IdPreFactura == preFactura.IdPreFactura)
            .Include(d => d.Servicios) 
            .AsNoTracking()
            .ToListAsync();

        await AfectarExistencia(detallesOriginales.ToArray(), TipoOperacion.Suma);

        contexto.preFacturaDetalles.RemoveRange(detallesOriginales);

        contexto.preFacturas.Update(preFactura);
        await AfectarExistencia(preFactura.PreFacturaDetalles.ToArray(), TipoOperacion.Resta);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(PreFacturas preFactura)
    {
        if (!await Existe(preFactura.IdPreFactura))
            return await Insertar(preFactura);
        else
            return await Modificar(preFactura);
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var preFactura = await Buscar(id);

        if (preFactura != null)
        {
            await AfectarExistencia(preFactura.PreFacturaDetalles.ToArray(), TipoOperacion.Suma);
            contexto.preFacturaDetalles.RemoveRange(preFactura.PreFacturaDetalles);
            contexto.preFacturas.Remove(preFactura);
            return await contexto.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<PreFacturas?> Buscar(int Id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.preFacturas
            .Include(p => p.PreFacturaDetalles)
            .ThenInclude(d => d.Servicios)
            .FirstOrDefaultAsync(p => p.IdPreFactura == Id);
    }

    public async Task<List<PreFacturas>> Listar(Expression<Func<PreFacturas, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.preFacturas
            .Include(p => p.PreFacturaDetalles)
            .Include(p => p.Clientes)
            .Include(p => p.Empleado)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<PreFacturaDetalles>> BuscarInventario(string query)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (string.IsNullOrWhiteSpace(query))
        {
            return new List<PreFacturaDetalles>();
        }

        var lowerQuery = query.ToLower();

        var serviciosEncontrados = await contexto.servicios
            .Include(s => s.Materiales) 
            .Where(s => s.Nombre.ToLower().Contains(lowerQuery) ||
                (s.TipoServicio != null && s.TipoServicio.ToLower().Contains(lowerQuery)))
            .AsNoTracking()
            .ToListAsync();

  
        var resultados = serviciosEncontrados.Select(s => new PreFacturaDetalles
        {
            IdServicio = s.IdServicio,
            PrecioUnitario = (decimal)s.Precio,
            Cantidad = 1,
            Servicios = s 
        })
        .OrderBy(i => i.Servicios?.Nombre)
        .ToList();

        return resultados;
    }

    public async Task<List<Servicios>> ListarServicios()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.servicios
            .Where(s => s.IdServicio > 0)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int?> BuscarIdServicioPorNombre(string nombreServicio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        string nombreLimpio = nombreServicio.Trim().ToLower();

        var servicio = await contexto.servicios
            .AsNoTracking()
            .FirstOrDefaultAsync(s =>
                s.Nombre != null &&
                s.Nombre.Trim().ToLower() == nombreLimpio
            );

        return servicio?.IdServicio;
    }


}

public enum TipoOperacion
{
    Suma = 1,
    Resta = 2
}
