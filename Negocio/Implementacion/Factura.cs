using Comun;
using Datos.Contratos;
using Entidades.DTO;
using Negocio.Contratos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Comun.Utilidades;

namespace Negocio.Implementacion
{
    public class Factura : IFactura
    {
        private readonly IAcceso _acceso;

        private readonly IFacturaDatos _facturaDatos;

        public Factura( IAcceso acceso, IFacturaDatos facturaDatos )
        {
            _acceso = acceso;
            _facturaDatos = facturaDatos;
        }

        public FacturaDTO ObtenerFactura(int id)
        {
            string[,] parametros = new string[1, 2];
            parametros[0, 0] = "PId";
            parametros[0, 1] = id.ToString();

            FacturaDTO factura = new FacturaDTO();

            DataRow tblResultado = (DataRow)_acceso.DevolverDatos(Query.OBTENER_FACTURA, parametros, Tipo.REGISTRO);

            if( tblResultado != null )
            {
                factura.Id = (int)tblResultado["id"];
                factura.IdCliente = (int)tblResultado["idCliente"];
                factura.CondicionesDePago = (string)tblResultado["condicionesDePago"].ToString();
                factura.FechaCreacion = (DateTime)tblResultado["fechaCreacion"];
                factura.FechaModificacion = (tblResultado["fechaModificacion"] is not DBNull) ? (DateTime)tblResultado["fechaModificacion"] : null;
                factura.FechaEliminacion = (tblResultado["fechaEliminacion"] is not DBNull) ? (DateTime)tblResultado["fechaEliminacion"] : null;
                factura.Estado = (bool)tblResultado["estado"];
            }

            return factura;
        }

        public List<FacturaDTO> ObtenerFacturas()
        {
            List<FacturaDTO> clientes = new List<FacturaDTO>();
            DataTable tblResultado = (DataTable)_acceso.DevolverDatos(Query.OBTENER_FACTURAS, null, Tipo.TABLA);

            if (tblResultado != null)
            {
                foreach (DataRow item in tblResultado.Rows)
                {
                    clientes.Add(
                        new FacturaDTO
                        {
                            Id = (int)item["id"],
                            IdCliente = (int)item["id"],
                            CondicionesDePago = (string)item["condicionesDePago"].ToString(),
                            FechaCreacion = (DateTime)item["fechaCreacion"],
                            Estado = (bool)item["estado"]
                        }
                    );
                }
            }

            return clientes;
        }

        public FacturaDTO GuardarFactura( FacturaDTO factura )
        {
            DataRow registro = (DataRow)_facturaDatos.GuardarFactura(factura);

            factura.Id = (int)registro["id"];
            
            return factura;
        }

        public FacturaDTO EditarFactura(FacturaDTO factura)
        {
            DataRow registro = _facturaDatos.EditarFactura( factura );

            factura.FechaModificacion = (DateTime)registro["fechaModificacion"];

            return factura;

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] 
            {
                new DataColumn("PidFactura"),
                new DataColumn("PidProducto"),
                new DataColumn("Pcantidad") 
            });

            dt.Rows.Add(1, "John Hammond", "United States");
            dt.Rows.Add(2, "Mudassar Khan", "India");
            dt.Rows.Add(3, "Suzanne Mathews", "France");
            dt.Rows.Add(4, "Robert Schidner", "Russia");

            string[] storeProcedure = new string[2];
            storeProcedure[0] = Query.EDITAR_FACTURA;
            storeProcedure[1] = Query.GUARDAR_DETALLE_FACTURA;

            string[,] maestro = new string[2, 2];
            maestro[0, 0] = "@PId";
            maestro[0, 1] = factura.Id.ToString();
            maestro[1, 0] = "@condicionesDePago";
            maestro[1, 1] = factura.CondicionesDePago.ToString();

            //facturaDetalle  = new DataTable();

            //column = new DataColumn();
            //column.DataType = Type.GetType("System.Int32");
            //column.ColumnName = "PidFactura";
            //facturaDetalle.Columns.Add(column);

            //column = new DataColumn();
            //column.DataType = Type.GetType("System.Int32");
            //column.ColumnName = "PidProducto";
            //facturaDetalle.Columns.Add(column);

            //column = new DataColumn();
            //column.DataType = Type.GetType("System.Int32");
            //column.ColumnName = "Pcantidad";
            //facturaDetalle.Columns.Add(column);


            //foreach ( FacturaDetalleDTO detalle in factura.facturaDetalles )
            //{
                
                //facturaDetalle.Rows.Add(detalle.IdFactura, detalle.IdProducto, detalle.Cantidad);
                //facturaDetalle.Rows.Add();

                //facturaDetalle.Rows[facturaDetalle.Rows.Count - 1]["PidFactura"] = detalle.IdFactura.ToString();
                //facturaDetalle.Rows[facturaDetalle.Rows.Count - 1]["PidProducto"] = detalle.IdProducto.ToString();
                //facturaDetalle.Rows[facturaDetalle.Rows.Count - 1]["Pcantidad"] = detalle.Cantidad.ToString();
            //}

            //Utilidades converter = new Utilidades();
            //DataTable detalles = converter.ToDataTable(facturaDetalle);

            //return _acceso.EjecutarConTransaccionConDetalle(storeProcedure, maestro, dt);
        }

        public FacturaDTO EliminarFactura(int id)
        {
            FacturaDTO factura = new FacturaDTO();

            string[,] parametros = new string[1, 2];

            parametros[0, 0] = "@PId";
            parametros[0, 1] = id.ToString();

            DataRow registro = (DataRow)_acceso.EjecutarConTransaccion(Query.ELIMINAR_FACTURA, parametros);

            factura.Id = (int)registro["id"];
            factura.IdCliente = (int)registro["nombre"];
            factura.FechaEliminacion = (DateTime)registro["fechaEliminacion"];

            return factura;
        }
    }
}
