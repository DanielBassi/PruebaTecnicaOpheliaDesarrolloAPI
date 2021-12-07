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
using static Comun.Utilidades;

namespace Datos.Implementacion
{
    public class Acceso : IAcceso
    {
        private readonly IConexion _conexion;

        public Acceso( IConexion conexion )
        {
            _conexion = conexion;
        }

        static void ObtenerParametros(ref SqlCommand sentencia, object[,] parametros)
        {
            if (parametros != null && parametros.Length > 0)
            {
                for (int indice = 0; indice < (parametros.Length / 2); indice++)
                {
                    sentencia.Parameters.AddWithValue(parametros[indice, 0].ToString(), parametros[indice, 1]);
                }
            }
        }

        public SqlCommand ObtenerComando(SqlConnection coneccion, string store_procedure, string[,] parametros)
        {
            SqlCommand comando = new SqlCommand();
            comando.Connection = coneccion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = store_procedure;
            ObtenerParametros(ref comando, parametros);
            return comando;
        }

        public object DevolverDatos(string storeProcedure, string[,] parametros, Tipo tipo)
        {
            DataSet dataSet = new DataSet();
            try
            {
                SqlCommand sentencia = null;
                sentencia = ObtenerComando(_conexion.DevolverConeccion(), storeProcedure, parametros);
                using (SqlDataAdapter adaptador = new SqlDataAdapter(sentencia))
                {
                    adaptador.Fill(dataSet);
                }
                sentencia.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return DefinirDatos(dataSet, tipo);
        }

        public static object DefinirDatos(DataSet dataSet, Tipo tipo)
        {
            object respuesta = null;
            if (dataSet != null & dataSet.Tables.Count > 0)
            {
                switch (tipo)
                {
                    case Tipo.TABLAS:
                        respuesta = dataSet;
                        break;
                    case Tipo.TABLA:
                        respuesta = dataSet.Tables[0];
                        break;
                    case Tipo.REGISTRO:
                        respuesta = (dataSet.Tables[0].Rows.Count > 0) ? dataSet.Tables[0].Rows[0] : null;
                        break;
                    default:
                        break;
                }
            }

            return respuesta;
        }

        public void EjecutarComando(string store_procedure, string[,] parametros)
        {
            try
            {
                SqlCommand sentencia = null;
                sentencia = ObtenerComando(_conexion.DevolverConeccion(), store_procedure, parametros);
                sentencia.ExecuteScalar();
                sentencia.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EjecutarConTransaccionConDetalle( string[] storeProcedure, string[,] maestro, DataTable detalles )
        {
            SqlTransaction trans = null;

            try
            {
                SqlCommand sentencia = ObtenerComando(_conexion.DevolverConeccion(), storeProcedure[0], maestro);
                trans = sentencia.Connection.BeginTransaction();
                sentencia.Transaction = trans;
                //sentencia.ExecuteNonQuery();

                foreach (DataRow row in detalles.Rows)
                {
                    sentencia.Parameters.Clear();
                    sentencia.CommandText = storeProcedure[1];
                    foreach (DataColumn column in detalles.Columns)
                    {
                        sentencia.Parameters.AddWithValue("@" + column.ColumnName, row[column].ToString());
                    }

                    sentencia.ExecuteNonQuery();
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
              return true;
            }
            catch (Exception ex)
            {
              if (trans != null)
              {
                trans.Rollback();
                trans.Dispose();
              }

              return false;
            }
    }

        public object EjecutarConTransaccion(string storeProcedure, string[,] parametros)
        {
            SqlTransaction trans = null;
            DataSet dataSet = new DataSet();

            try
            {
                SqlCommand sentencia = ObtenerComando(_conexion.DevolverConeccion(), storeProcedure, parametros);
                trans = sentencia.Connection.BeginTransaction();
                sentencia.Transaction = trans;
                //sentencia.ExecuteNonQuery();
                using (SqlDataAdapter adaptador = new SqlDataAdapter(sentencia))
                {
                    adaptador.Fill(dataSet);
                }

                sentencia.Dispose();

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

                return DefinirDatos(dataSet, Tipo.REGISTRO);
                //return true;

            }
            catch (Exception ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                    trans.Dispose();
                }

                return false;
            }
        }

    }
}
