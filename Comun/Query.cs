using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun
{
    public class Query
    {
        /* Clientes */
        public const string OBTENER_CLIENTES = "sp_listarClientes";
        public const string OBTENER_CLIENTE = "sp_obtenerClientePorId";
        public const string GUARDAR_CLIENTE = "sp_guardarCliente";
        public const string EDITAR_CLIENTE = "sp_editarCliente";
        public const string ELIMINAR_CLIENTE = "sp_eliminarCliente";

        /* Facturas */
        public const string OBTENER_FACTURAS = "sp_listarFacturas";
        public const string OBTENER_FACTURA = "sp_obtenerFacturaPorId";
        public const string GUARDAR_FACTURA = "sp_guardarFactura";
        public const string EDITAR_FACTURA = "sp_editarFactura";
        public const string ELIMINAR_FACTURA = "sp_eliminarFactura";

        /* DetalleFactura */
        public const string GUARDAR_DETALLE_FACTURA = "sp_guardarFacturaDetalle";

        /* Productos */
        public const string OBTENER_PRODUCTOS = "sp_listarProductos";

        /* Inventario */
        public const string OBTENER_INVENTARIO = "sp_listarInventario";
    }
}
