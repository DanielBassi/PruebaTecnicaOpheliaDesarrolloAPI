using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Contratos
{
    public interface ICliente
    {
        public List<ClienteDTO> ObtenerClientes( string[,] parametros = null);

        public ClienteDTO ObtenerCliente( int id );

        public ClienteDTO GuardarCliente( ClienteDTO cliente );

        public ClienteDTO EditarCliente( ClienteDTO cliente );

        public ClienteDTO EliminarCliente( int id );
    }
}
