using KCR.Data;
using KCR.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KCR.Services;

public class ServiciosService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
    public async Task<bool> Existe(int idservicio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.servicios.AnyAsync(s => s.IdServicio == idservicio);
    }
    public async Task<bool> ExisteNombre(string nombre, int idActual = 0)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.servicios
            .AnyAsync(s => s.Nombre.ToLower() == nombre.ToLower() && s.IdServicio != idActual);
    }

    public async Task<bool> Insertar(Servicios servicio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.servicios.Add(servicio);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Servicios servicio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.servicios.Update(servicio);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Servicios servicio)
    {
        if (!await Existe(servicio.IdServicio))
            return await Insertar(servicio);
        else
            return await Modificar(servicio);
    }

    public async Task<bool> Eliminar(int idservicio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.servicios.Where(s => s.IdServicio == idservicio).AsNoTracking().ExecuteDeleteAsync() > 0;
    }

    public async Task<Servicios?> Buscar(int idservicio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.servicios
            .Include(s => s.Materiales)
            .FirstOrDefaultAsync(s => s.IdServicio == idservicio);
    }

    public async Task<List<Servicios>> Listar(Expression<Func<Servicios, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.servicios.Where(criterio).AsNoTracking().ToListAsync();
    }
    public async Task<List<Materiales>> BuscarMaterialesPorNombre(string query)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        if (string.IsNullOrWhiteSpace(query))
            return new List<Materiales>();

        return await contexto.materiales
            .Where(m => m.Activo && m.Nombre.ToLower().Contains(query.ToLower()))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Servicios>> ListarTodoConMateriales()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.servicios
            .Include(s => s.Materiales)
            .AsNoTracking()
            .ToListAsync();
    }
}
