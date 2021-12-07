using Entidades.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Contratos
{
    public interface IInventario
    {
        public List<InventarioDTO> ObtenerInvenatrios( string[,] parametros = null );
    }
}
