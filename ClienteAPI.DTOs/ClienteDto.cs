using System.ComponentModel.DataAnnotations;

namespace ClienteAPI.DTOs
{
    public class ClienteDto
    {
        [Required]
        public int Identificacion { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(50)]
        [EmailAddress]
        public string Correo { get; set; }
    }

    public class ClienteResponseDto
    {
        public bool Existe { get; set; }
        public ClienteDto Cliente { get; set; }
        public string Mensaje { get; set; }
    }
}