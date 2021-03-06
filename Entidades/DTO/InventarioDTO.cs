using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTO
{
    public class InventarioDTO
    {
        [Key]
        public int Id { get; set; }

        public int IdProducto { get; set; }

        public decimal PrecioCompra { get; set; }

        public decimal PrecioVenta { get; set; }

        public int Cantidad { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public DateTime? FechaEliminacion { get; set; }

        public bool Estado { get; set; }
    }
}
