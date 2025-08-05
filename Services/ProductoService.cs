using BarbudosShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using BarbudosShop.Data;

namespace BarbudosShop.Services;

public class ProductoService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

    public ProductoService(IDbContextFactory<ApplicationDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> Guardar(Producto producto)
    {
        if (!await Existe(producto.IdProducto))
            return await Insertar(producto);
        else
            return await Modificar(producto);
    }

    private async Task<bool> Existe(int productoId)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.Productos.AnyAsync(p => p.IdProducto == productoId);
    }

    private async Task<bool> Insertar(Producto producto)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        contexto.Productos.Add(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Producto producto)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        contexto.Productos.Update(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Producto?> Buscar(int productoId)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.Productos.FirstOrDefaultAsync(p => p.IdProducto == productoId);
    }

    public async Task<bool> Eliminar(int productoId)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        var producto = await contexto.Productos.FirstOrDefaultAsync(p => p.IdProducto == productoId);
        if (producto != null)
        {
            contexto.Productos.Remove(producto);
            return await contexto.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<Producto>> Listar(Expression<Func<Producto, bool>> criterio)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.Productos.Where(criterio).Include(p => p.Categoria).ToListAsync();
    }

    public async Task<bool> ExisteProducto(int productoId, string nombre)
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.Productos
            .AnyAsync(p => p.IdProducto != productoId && p.Nombre.ToLower() == nombre.ToLower());
    }

    public async Task<List<Producto>> GetProductosAsync()
    {
        await using var contexto = _dbFactory.CreateDbContext();
        return await contexto.Productos
            .Include(p => p.Categoria)
            .Where(p => p.Disponible)
            .OrderBy(p => p.Nombre)
            .ToListAsync();
    }
}
