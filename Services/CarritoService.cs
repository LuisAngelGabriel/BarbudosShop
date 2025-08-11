using Microsoft.EntityFrameworkCore;
using BarbudosShop.Data;
using BarbudosShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarbudosShop.Services
{
    public class CarritoService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public event Action? OnChange;

        public CarritoService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<bool> Agregar(ItemCarrito item)
        {
            await using var contexto = _dbFactory.CreateDbContext();

            var itemExistente = await contexto.ItemsCarrito
                .FirstOrDefaultAsync(i => i.IdUsuario == item.IdUsuario && i.IdProducto == item.IdProducto);

            if (itemExistente == null)
            {
                item.Cantidad = 1;
                contexto.ItemsCarrito.Add(item);
            }
            else
            {
                itemExistente.Cantidad++;
                contexto.ItemsCarrito.Update(itemExistente);
            }

            var resultado = await contexto.SaveChangesAsync() > 0;

            if (resultado)
            {
                NotifyStateChanged();
            }

            return resultado;
        }

        public async Task<List<ItemCarrito>> ListarPorUsuario(string idUsuario)
        {
            await using var contexto = _dbFactory.CreateDbContext();
            return await contexto.ItemsCarrito
                .Include(i => i.Producto)
                .Include(i => i.Producto!.Categoria)
                .Where(i => i.IdUsuario == idUsuario)
                .ToListAsync();
        }

        public async Task<bool> Eliminar(int id)
        {
            await using var contexto = _dbFactory.CreateDbContext();
            var item = await contexto.ItemsCarrito.FirstOrDefaultAsync(i => i.Id == id);
            if (item != null)
            {
                contexto.ItemsCarrito.Remove(item);
                var resultado = await contexto.SaveChangesAsync() > 0;

                if (resultado)
                {
                    NotifyStateChanged();
                }
                return resultado;
            }
            return false;
        }

        public async Task<bool> Vaciar(string idUsuario)
        {
            await using var contexto = _dbFactory.CreateDbContext();
            var items = await contexto.ItemsCarrito
                .Where(i => i.IdUsuario == idUsuario)
                .ToListAsync();

            if (items.Any())
            {
                contexto.ItemsCarrito.RemoveRange(items);
                var resultado = await contexto.SaveChangesAsync() > 0;

                if (resultado)
                {
                    NotifyStateChanged();
                }
                return resultado;
            }
            return false;
        }

        public async Task<int> ObtenerCantidad(string idUsuario)
        {
            await using var contexto = _dbFactory.CreateDbContext();
            return await contexto.ItemsCarrito
                .Where(i => i.IdUsuario == idUsuario)
                .SumAsync(i => i.Cantidad);
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
        public async Task<bool> ActualizarCantidad(int idItem, int nuevaCantidad)
        {
            await using var contexto = _dbFactory.CreateDbContext();
            var item = await contexto.ItemsCarrito.FirstOrDefaultAsync(i => i.Id == idItem);

            if (item != null)
            {
                if (nuevaCantidad > 0)
                {
                    item.Cantidad = nuevaCantidad;
                    contexto.ItemsCarrito.Update(item);
                }
                else
                {
                    contexto.ItemsCarrito.Remove(item);
                }

                var resultado = await contexto.SaveChangesAsync() > 0;
                if (resultado)
                {
                    NotifyStateChanged();
                }
                return resultado;
            }
            return false;
        }
    }
}