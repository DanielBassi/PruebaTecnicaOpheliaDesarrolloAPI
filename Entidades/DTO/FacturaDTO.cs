using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO
{
    public class FacturaDTO
    {
        [Key]
        public int Id { get; set; }

        public int IdCliente { get; set; } 

        public string CondicionesDePago { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public DateTime? FechaEliminacion { get; set; }

        public bool Estado { get; set; }

        public List<ClienteDTO> Clientes { get; set; }

        public List<FacturaDetalleDTO> facturaDetalles { get; set; }

    }
}
