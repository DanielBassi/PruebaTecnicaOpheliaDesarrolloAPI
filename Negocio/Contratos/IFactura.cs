using Entidades.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Contratos
{
    public interface IFactura
    {
        public List<FacturaDTO> ObtenerFacturas();

        public FacturaDTO ObtenerFactura( int id );

        public FacturaDTO GuardarFactura( FacturaDTO factura );

        public FacturaDTO EditarFactura( FacturaDTO factura );

        public FacturaDTO EliminarFactura( int id );
    }
}
