using Entidades.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Contratos
{
    public interface IFacturaDatos
    {
        public DataRow GuardarFactura( FacturaDTO factura );

        public DataRow EditarFactura( FacturaDTO factura );
    }
}
