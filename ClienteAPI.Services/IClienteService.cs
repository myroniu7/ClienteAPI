using ClienteAPI.DTOs;
using ClienteAPI.Data.Repositories;

namespace ClienteAPI.Services
{
    public interface IClienteService
    {
        Task<ClienteResponseDto> ObtenerClientePorIdentificacionAsync(int identificacion);
    }

    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteResponseDto> ObtenerClientePorIdentificacionAsync(int identificacion)
        {
            var clienteEntity = await _clienteRepository.ObtenerClientePorIdentificacionAsync(identificacion);

            if (clienteEntity == null)
            {
                return new ClienteResponseDto
                {
                    Existe = false,
                    Mensaje = "Cliente no encontrado"
                };
            }

            return new ClienteResponseDto
            {
                Existe = true,
                Cliente = new ClienteDto
                {
                    Identificacion = clienteEntity.Identificacion,
                    Nombre = clienteEntity.Nombre,
                    Correo = clienteEntity.Correo
                },
                Mensaje = "Cliente encontrado exitosamente"
            };
        }
    }
}