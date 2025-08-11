using BarbudosShop.Data;
using BarbudosShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BarbudosShop.Services
{
    public class PedidoService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public PedidoService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<bool> ProcesarPedido(string idUsuario, DireccionEnvioViewModel direccion, List<ItemCarrito> items)
        {
            if (items == null || !items.Any())
            {
                return false; 
            }

            try
            {
                await using var contexto = _dbFactory.CreateDbContext();

                var nuevoPedido = new Pedido
                {
                    IdUsuario = idUsuario,
                    FechaPedido = DateTime.Now,
                    Calle = direccion.Calle,
                    Sector = direccion.Sector,
                    Ciudad = direccion.Ciudad,
                    Provincia = direccion.Provincia,
                    Telefono = direccion.Telefono,
                    Total = items.Sum(i => i.Cantidad * (i.Producto?.Precio ?? 0))
                };

                contexto.Pedidos.Add(nuevoPedido);

                foreach (var item in items)
                {
                    var producto = await contexto.Productos.FindAsync(item.IdProducto);

                    if (producto == null || producto.Cantidad < item.Cantidad)
                    {
                    
                        return false;
                    }

                    producto.Cantidad -= item.Cantidad;

                    var detalle = new DetallePedido
                    {
                        IdPedido = nuevoPedido.Id,
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.Producto?.Precio ?? 0,
                        Pedido = nuevoPedido 
                    };

                    contexto.DetallesPedido.Add(detalle);
                }

                await contexto.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
             
                return false;
            }
        }

        public async Task<List<Pedido>> ListarPedidosPorUsuario(string idUsuario)
        {
            await using var contexto = _dbFactory.CreateDbContext();
            return await contexto.Pedidos
                .Include(p => p.Detalles)!
                .ThenInclude(d => d.Producto)
                .Where(p => p.IdUsuario == idUsuario)
                .OrderByDescending(p => p.FechaPedido)
                .ToListAsync();
        }

        public async Task<List<Pedido>> ListarTodosLosPedidosAsync()
        {
            await using var contexto = _dbFactory.CreateDbContext();

            return await contexto.Pedidos
                .Include(p => p.Detalles)
                .ThenInclude(d => d.Producto)
                .OrderByDescending(p => p.FechaPedido)
                .ToListAsync();
        }
    }
}