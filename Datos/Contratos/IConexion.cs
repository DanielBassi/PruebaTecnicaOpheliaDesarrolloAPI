using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Contratos
{
    public interface IConexion
    {
        SqlConnection DevolverConeccion();
    }
}
