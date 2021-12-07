using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Comun.Utilidades;

namespace Datos.Contratos
{
    public interface IAcceso
    {
        object DevolverDatos(string storeProcedure, string[,] parametros, Tipo tipo);

        public void EjecutarComando(string storeProcedure, string[,] parametros);

        public object EjecutarConTransaccion( string storeProcedure, string[,] parametros );

        public bool EjecutarConTransaccionConDetalle(string[] storeProcedure, string[,] maestro, DataTable detalle );
    }
}
