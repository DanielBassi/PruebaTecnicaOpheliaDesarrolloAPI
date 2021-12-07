using Comun;
using Datos.Contratos;
using Entidades.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Implementacion
{
    public class FacturaDatos: IFacturaDatos
    {
        private readonly IConexion _conexion;

        public FacturaDatos( IConexion conexion )
        {
            _conexion = conexion;
        }

        public DataRow GuardarFactura( FacturaDTO factura )
        {
            SqlTransaction trans = null;
            DataSet dataSet = new DataSet();
            try
            {

                using (SqlCommand sentencia = new SqlCommand())
                {
                    sentencia.Connection = _conexion.DevolverConeccion();
                    trans = sentencia.Connection.BeginTransaction();
                    sentencia.Transaction = trans;

                    sentencia.CommandType = CommandType.StoredProcedure;
                    sentencia.CommandText = Query.GUARDAR_FACTURA;
                    sentencia.Parameters.AddWithValue("@IdCliente", factura.IdCliente);
                    sentencia.Parameters.AddWithValue("@PCondicionesDePago", factura.CondicionesDePago);

                    //factura.Id = int.Parse(sentencia.ExecuteScalar().ToString());

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(sentencia))
                    {
                        adaptador.Fill(dataSet);
                    }

                    foreach (FacturaDetalleDTO detalle in factura.facturaDetalles)
                    {
                        sentencia.Parameters.Clear();
                        sentencia.CommandText = Query.GUARDAR_DETALLE_FACTURA;
                        sentencia.Parameters.AddWithValue("@PidFactura", dataSet.Tables[0].Rows[0]["id"]);
                        sentencia.Parameters.AddWithValue("@PidProducto", detalle.IdProducto);
                        sentencia.Parameters.AddWithValue("@Pcantidad", detalle.Cantidad);
                        sentencia.ExecuteNonQuery();
                    }
                }

                try
                {
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
                finally
                {
                    if (trans != null)
                    {
                        trans.Dispose();
                    }

                }

                return dataSet.Tables[0].Rows[0];
                //return true;
            }
            catch (Exception ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                    trans.Dispose();
                }

                return null;
            }
        }

        public DataRow EditarFactura( FacturaDTO factura )
        {
            SqlTransaction trans = null;
            DataSet dataSet = new DataSet();
            try
            {

                using (SqlCommand sentencia = new SqlCommand())
                {
                    sentencia.Connection = _conexion.DevolverConeccion();
                    trans = sentencia.Connection.BeginTransaction();
                    sentencia.Transaction = trans;

                    sentencia.CommandType = CommandType.StoredProcedure;
                    sentencia.CommandText = Query.EDITAR_FACTURA;
                    sentencia.Parameters.AddWithValue("@PId", factura.Id);
                    sentencia.Parameters.AddWithValue("@PCondicionesDePago", factura.CondicionesDePago);

                    //factura.Id = int.Parse(sentencia.ExecuteScalar().ToString());

                    using (SqlDataAdapter adaptador = new SqlDataAdapter(sentencia))
                    {
                        adaptador.Fill(dataSet);
                    }

                    foreach (FacturaDetalleDTO detalle in factura.facturaDetalles)
                    {
                        sentencia.Parameters.Clear();
                        sentencia.CommandText = Query.GUARDAR_DETALLE_FACTURA;
                        sentencia.Parameters.AddWithValue("@PidFactura", factura.Id);
                        sentencia.Parameters.AddWithValue("@PidProducto", detalle.IdProducto);
                        sentencia.Parameters.AddWithValue("@Pcantidad", detalle.Cantidad);
                        sentencia.ExecuteNonQuery();
                    }
                }

                try
                {
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
                finally
                {
                    if (trans != null)
                    {
                        trans.Dispose();
                    }

                }

                return dataSet.Tables[0].Rows[0];
                //return true;
            }
            catch (Exception ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                    trans.Dispose();
                }

                return null;
            }
        }

    }
}
