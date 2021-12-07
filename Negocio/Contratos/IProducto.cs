using Entidades.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Contratos
{
    public interface IProducto
    {
        public List<ProductoDTO> ObtenerProductos( string[,] parametros = null );
    }
}
