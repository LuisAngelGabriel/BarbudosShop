using BarbudosShop.Data;
using BarbudosShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BarbudosShop.Services
{
    public class SugerenciaService
    {
        private readonly ApplicationDbContext _context;

        public SugerenciaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Guardar(Sugerencia sugerencia)
        {
            if (sugerencia.SugerenciaId == 0)
            {
                _context.Sugerencias.Add(sugerencia);
            }
            else
            {
                _context.Sugerencias.Update(sugerencia);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Sugerencia?> Buscar(int id)
        {
            return await _context.Sugerencias.FindAsync(id);
        }

        public async Task<List<Sugerencia>> Listar(Expression<Func<Sugerencia, bool>> predicado)
        {
            return await _context.Sugerencias.AsNoTracking()
                .Where(predicado)
                .OrderByDescending(s => s.Fecha)
                .ToListAsync();
        }

        public async Task<bool> Eliminar(int id)
        {
            var sugerencia = await _context.Sugerencias.FindAsync(id);
            if (sugerencia == null) return false;

            _context.Sugerencias.Remove(sugerencia);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}