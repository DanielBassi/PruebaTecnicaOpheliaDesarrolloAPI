using Datos.Contratos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Implementacion
{
    public class Conexion: IConexion
    {
        SqlConnection con;

        private SqlConnection ObtenerConexion()
        {
            con = new SqlConnection("server=localhost\\SQLEXPRESS;user=sa;database=PruebaTecnicaOpheliaDesarrolloDB;password=Sabanalarga1998");
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }


        public SqlConnection DevolverConeccion()
        {
            return ObtenerConexion();
        }
    }
}
