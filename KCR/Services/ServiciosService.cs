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
        return await contexto.servicios.FirstOrDefaultAsync(s => s.IdServicio == idservicio);
    }

    public async Task<List<Servicios>> Listar(Expression<Func<Servicios, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.servicios.Include(s => s.Materiales).Where(criterio).AsNoTracking().ToListAsync();
    }
}
