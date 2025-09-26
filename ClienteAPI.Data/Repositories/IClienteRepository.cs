using Microsoft.EntityFrameworkCore;
using ClienteAPI.Data.Entities;

namespace ClienteAPI.Data.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente> ObtenerClientePorIdentificacionAsync(int identificacion);
    }

    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente> ObtenerClientePorIdentificacionAsync(int identificacion)
        {
            try
            {
                var result = await _context.Clientes
                    .FromSqlInterpolated($"EXEC sp_ConsultarCliente {identificacion}")
                    .AsNoTracking()
                    .ToListAsync();

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Log the error (deberías usar ILogger en producción)
                throw new ApplicationException($"Error al consultar cliente: {ex.Message}", ex);
            }
        }
    }
}